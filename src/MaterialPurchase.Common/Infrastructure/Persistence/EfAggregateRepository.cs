using MaterialPurchase.Common.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaterialPurchase.Common.Infrastructure.Persistence;

public class EfAggregateRepository<TAggregateRoot, TDbContext> : IAggregateRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
    where TDbContext : DbContext
{
    readonly TDbContext _dbContext;

    public EfAggregateRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TAggregateRoot?> GetById(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Set<TAggregateRoot>().FindAsync([id], cancellationToken: cancellationToken);

    public Task GetByIds(ICollection<Guid> ids, CancellationToken cancellationToken)
    {
        return _dbContext.Set<TAggregateRoot>().Where(aggregate => ids.Contains(aggregate.Id)).ToListAsync(cancellationToken);
    }

    public async Task<TAggregateRoot?> FirstOrDefault(Expression<Func<TAggregateRoot, bool>> filterExpression,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TAggregateRoot>().FirstOrDefaultAsync(filterExpression, cancellationToken);
    }

    public async Task<ICollection<TAggregateRoot>> FilterBy(Expression<Func<TAggregateRoot, bool>> filterExpression,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TAggregateRoot>().Where(filterExpression).ToListAsync(cancellationToken);
    }

    public void Add(TAggregateRoot entity) => _dbContext.Add(entity);
    public void AddRange(ICollection<TAggregateRoot> entities) => _dbContext.AddRange(entities);
    public void Remove(TAggregateRoot entity) => _dbContext.Remove(entity);
    public void RemoveRange(ICollection<TAggregateRoot> entities) => _dbContext.RemoveRange(entities);
}