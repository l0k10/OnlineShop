using FluentAssertions;
using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Tests.Unit.Domain;

/// <summary>
/// Unit tests for the Price value object.
/// </summary>
public class PriceTests
{
    [Fact]
    public void Create_WithValidAmountAndCurrency_ShouldCreatePrice()
    {
        // Arrange
        var amount = 99.99m;
        var currency = "EUR";

        // Act
        var price = new Price(amount, currency);

        // Assert
        price.Amount.Should().Be(amount);
        price.Currency.Should().Be(currency);
    }

    [Fact]
    public void Zero_ShouldReturnZeroPriceInEUR()
    {
        // Act
        var zeroPrice = Price.Zero;

        // Assert
        zeroPrice.Amount.Should().Be(0);
        zeroPrice.Currency.Should().Be("EUR");
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var price = new Price(99.99m, "EUR");

        // Act
        var result = price.ToString();

        // Assert
        result.Should().Contain("99.99");
        result.Should().Contain("EUR");
    }

    [Theory]
    [InlineData(0, "USD")]
    [InlineData(100.50, "GBP")]
    [InlineData(0.01, "JPY")]
    public void Create_WithDifferentCurrencies_ShouldCreateCorrectPrice(decimal amount, string currency)
    {
        // Act
        var price = new Price(amount, currency);

        // Assert
        price.Amount.Should().Be(amount);
        price.Currency.Should().Be(currency);
    }
}
