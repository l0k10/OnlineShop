namespace ProductCatalog.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value with amount and currency.
/// </summary>
/// <param name="Amount">The monetary amount.</param>
/// <param name="Currency">The currency code (e.g., "EUR", "USD").</param>
public record Price(decimal Amount, string Currency)
{
    /// <summary>
    /// Gets a zero price in EUR currency.
    /// </summary>
    public static Price Zero => new(0, "EUR");

    /// <summary>
    /// Returns a string representation of the price.
    /// </summary>
    /// <returns>A formatted string showing the amount and currency.</returns>
    public override string ToString() => $"{Amount:C} {Currency}";
}
