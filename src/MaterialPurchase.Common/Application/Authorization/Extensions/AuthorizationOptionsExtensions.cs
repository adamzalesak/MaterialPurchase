using Microsoft.AspNetCore.Authorization;

namespace MaterialPurchase.Common.Application.Authorization.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static AuthorizationOptions AddPurchasePolicy(this AuthorizationOptions authorizationOptions, string policyName, params string[] allowedRoles)
    {
        authorizationOptions.AddPolicy(policyName, policy =>
        {
            policy.Requirements.Add(new ExistingDbUserRequirement());

            var roles = allowedRoles.ToList();

            if (!roles.Contains(AppRoles.Admin))
            {
                roles.Add(AppRoles.Admin);
            }

            policy.RequireRole(roles);
        });

        return authorizationOptions;
    }
}