namespace MaterialPurchase.Orders.Infrastructure.Persistence;

public interface IOrderReadRepository
{
    public Task<bool> ExistsByOrderCartId(Guid orderCartId, CancellationToken cancellationToken);
}