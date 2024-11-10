using MaterialPurchase.Common.Application.Authorization;
using MaterialPurchase.Common.Entities;
using MaterialPurchase.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace MaterialPurchase.Handlers;

public class ExistingDbUserAuthorizationHandler : AuthorizationHandler<ExistingDbUserRequirement>
{
    private const int _accessTokenExpirationMinutesSubtract = 60;
    private const string _sidClaimType = "onprem_sid";

    readonly ILogger<ExistingDbUserAuthorizationHandler> _logger;
    readonly IMemoryCache _memoryCache;
    readonly CommonDbContext _dbContext;

    public ExistingDbUserAuthorizationHandler(ILogger<ExistingDbUserAuthorizationHandler> logger, IMemoryCache memoryCache, CommonDbContext dbContext)
    {
        _logger = logger;
        _memoryCache = memoryCache;
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ExistingDbUserRequirement requirement)
    {
        ArgumentNullException.ThrowIfNull(context);

        var claimValue = context.User.Claims.FirstOrDefault(claim => claim.Type == _sidClaimType)?.Value;
        if (claimValue is null)
        {
            _logger.LogInformation("User without Sid claim cannot be authorized.");
            context.Fail();

            return;
        }

        var dbUser = await _memoryCache.GetOrCreateAsync(claimValue, entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(_accessTokenExpirationMinutesSubtract);
                
                return GetUserFromDb(claimValue, CancellationToken.None);
            }
        );

        if (dbUser is null)
        {
            _logger.LogInformation("User with sid = '{ClaimValue}' could not be found in Purchase DB and authorized", claimValue);
            context.Fail();
            return;
        }

        var identity = context.User.Identities.FirstOrDefault();

        if (identity is null)
        {
            _logger.LogError("User with sid = '{ClaimValue}' has no identity", claimValue);
            context.Fail();
            return;
        }

        var userIdClaims = identity.Claims.Where(claim => claim.Type == UserClaimTypes.UserIdClaim).ToList();

        if (userIdClaims.Any())
        {
            userIdClaims.ForEach(identity.RemoveClaim);
        }
        identity.AddClaim(CreateUserIdClaim(dbUser.Id));

        context.Succeed(requirement);
    }

    private async Task<User?> GetUserFromDb(string sid, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.SId == sid && x.IsEnabled, cancellationToken);

        return user;
    }
    
    private static Claim CreateUserIdClaim(int userId)
    {
        return new Claim(UserClaimTypes.UserIdClaim, userId.ToString(), ClaimValueTypes.Integer);
    }
}