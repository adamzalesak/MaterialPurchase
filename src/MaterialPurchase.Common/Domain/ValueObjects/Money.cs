namespace MaterialPurchase.Common.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (currency.Length != 3)
        {
            throw new ArgumentException("Currency must be 3 characters long");
        }

        Amount = amount;
        Currency = currency;
    }

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
    
    public static Money operator *(Money money, decimal multiplier)
    {
        return money with { Amount = money.Amount * multiplier };
    }
    
    public static Money operator /(Money money, decimal divisor)
    {
        return money with { Amount = money.Amount / divisor };
    }
}