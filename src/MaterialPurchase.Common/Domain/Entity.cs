namespace MaterialPurchase.Common.Domain;

public abstract class Entity<TId>
{
    public TId Id { get; set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity()
    {
        if (typeof(TId) == typeof(Guid))
        {
            Id = (TId)(object)Guid.NewGuid();
        }
        else
        {
            Id = default!;
        }
    }
}