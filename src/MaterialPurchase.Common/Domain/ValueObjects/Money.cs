namespace MaterialPurchase.Common.Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public Money(decimal amount) : this(amount, "EUR")
    {
    }
    
    public static Money operator +(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Cannot add money with different currencies");
        }

        return money1 with { Amount = money1.Amount + money2.Amount };
    }
    
    public static Money operator -(Money money1, Money money2)
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException("Cannot subtract money with different currencies");
        }

        return money1 with { Amount = money1.Amount - money2.Amount };
    }
}