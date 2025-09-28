using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Events;
using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Tests.Unit.Domain;

/// <summary>
/// Unit tests for the Product aggregate root.
/// </summary>
public class ProductTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldCreateProduct()
    {
        // Arrange
        var name = "Test Product";
        var description = "Test Description";
        var price = new Price(99.99m, "EUR");
        var initialStock = 10;

        // Act
        var product = Product.Create(name, description, price, initialStock);

        // Assert
        product.Name.Should().Be(name);
        product.Description.Should().Be(description);
        product.Price.Should().Be(price);
        product.Stock.Should().Be(initialStock);
        product.IsAvailable.Should().BeTrue();
        product.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Create_WithValidParameters_ShouldRaiseProductAddedEvent()
    {
        // Arrange
        var name = "Test Product";
        var description = "Test Description";
        var price = new Price(99.99m, "EUR");
        var initialStock = 10;

        // Act
        var product = Product.Create(name, description, price, initialStock);

        // Assert
        product.DomainEvents.Should().HaveCount(1);
        var domainEvent = product.DomainEvents.First() as ProductAdded;
        domainEvent.Should().NotBeNull();
        domainEvent!.Name.Should().Be(name);
        domainEvent.Description.Should().Be(description);
        domainEvent.Price.Should().Be(price.Amount);
        domainEvent.Currency.Should().Be(price.Currency);
        domainEvent.InitialStock.Should().Be(initialStock);
    }

    [Fact]
    public void ChangePrice_WithNewPrice_ShouldUpdatePriceAndRaiseEvent()
    {
        // Arrange
        var product = CreateTestProduct();
        var newPrice = new Price(149.99m, "USD");

        // Act
        product.ChangePrice(newPrice);

        // Assert
        product.Price.Should().Be(newPrice);
        product.DomainEvents.Should().HaveCount(2); // ProductAdded + PriceChanged
        var priceChangedEvent = product.DomainEvents.OfType<PriceChanged>().First();
        priceChangedEvent.ProductId.Should().Be(product.Id);
        priceChangedEvent.NewPrice.Should().Be(newPrice.Amount);
        priceChangedEvent.Currency.Should().Be(newPrice.Currency);
    }

    [Fact]
    public void UpdateStock_WithNewStock_ShouldUpdateStockAndRaiseEvent()
    {
        // Arrange
        var product = CreateTestProduct();
        var newStock = 5;

        // Act
        product.UpdateStock(newStock);

        // Assert
        product.Stock.Should().Be(newStock);
        product.DomainEvents.Should().HaveCount(2); // ProductAdded + StockUpdated
        var stockUpdatedEvent = product.DomainEvents.OfType<StockUpdated>().First();
        stockUpdatedEvent.ProductId.Should().Be(product.Id);
        stockUpdatedEvent.NewStock.Should().Be(newStock);
    }

    [Fact]
    public void UpdateStock_ToZero_ShouldRaiseProductBecameUnavailableEvent()
    {
        // Arrange
        var product = CreateTestProduct();

        // Act
        product.UpdateStock(0);

        // Assert
        product.Stock.Should().Be(0);
        product.IsAvailable.Should().BeFalse();
        product.DomainEvents.Should().HaveCount(3); // ProductAdded + StockUpdated + ProductBecameUnavailable
        var unavailableEvent = product.DomainEvents.OfType<ProductBecameUnavailable>().First();
        unavailableEvent.ProductId.Should().Be(product.Id);
    }

    [Fact]
    public void IsAvailable_WithStockGreaterThanZero_ShouldReturnTrue()
    {
        // Arrange
        var product = CreateTestProduct();

        // Act & Assert
        product.IsAvailable.Should().BeTrue();
    }

    [Fact]
    public void IsAvailable_WithZeroStock_ShouldReturnFalse()
    {
        // Arrange
        var product = CreateTestProduct();
        product.UpdateStock(0);

        // Act & Assert
        product.IsAvailable.Should().BeFalse();
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        // Arrange
        var product = CreateTestProduct();
        product.ChangePrice(new Price(200m, "EUR"));

        // Act
        product.ClearDomainEvents();

        // Assert
        product.DomainEvents.Should().BeEmpty();
    }

    private static Product CreateTestProduct()
    {
        return Product.Create(
            "Test Product",
            "Test Description",
            new Price(99.99m, "EUR"),
            10);
    }
}
