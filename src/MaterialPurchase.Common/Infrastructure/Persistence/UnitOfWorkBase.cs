using Microsoft.EntityFrameworkCore;

namespace MaterialPurchase.Common.Infrastructure.Persistence;

public abstract class UnitOfWorkBase
{
    protected UnitOfWorkBase(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    private DbContext DbContext { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}