namespace ProductCatalog.Domain.ValueObjects;

public record Price(decimal Amount, string Currency)
{
    public static Price Zero => new(0, "EUR");

    public override string ToString() => $"{Amount:C} {Currency}";
}
