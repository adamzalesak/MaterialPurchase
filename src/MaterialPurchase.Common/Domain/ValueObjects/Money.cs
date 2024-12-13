namespace MaterialPurchase.Common.Domain.ValueObjects;

public record Money(decimal Value, string Currency)
{
    public Money(decimal value) : this(value, "EUR")
    {
    }
    
    public static Money operator +(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Cannot add money with different currencies");
        }

        return money1 with { Value = money1.Value + money2.Value };
    }
    
    public static Money operator -(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Cannot subtract money with different currencies");
        }

        return money1 with { Value = money1.Value - money2.Value };
    }
}