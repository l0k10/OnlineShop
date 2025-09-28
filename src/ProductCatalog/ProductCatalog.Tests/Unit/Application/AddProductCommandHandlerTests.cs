using FluentAssertions;
using Moq;
using ProductCatalog.Application.Features.AddProduct;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Repositories;

namespace ProductCatalog.Tests.Unit.Application;

/// <summary>
/// Unit tests for the AddProductCommandHandler.
/// </summary>
public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly AddProductCommandHandler _handler;

    public AddProductCommandHandlerTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _handler = new AddProductCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateProductAndReturnId()
    {
        // Arrange
        var command = new AddProductCommand(
            "Test Product",
            "Test Description",
            99.99m,
            "EUR",
            10);

        Product? capturedProduct = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(p => capturedProduct = p)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        capturedProduct.Should().NotBeNull();
        capturedProduct!.Name.Should().Be(command.Name);
        capturedProduct.Description.Should().Be(command.Description);
        capturedProduct.Price.Amount.Should().Be(command.PriceAmount);
        capturedProduct.Price.Currency.Should().Be(command.Currency);
        capturedProduct.Stock.Should().Be(command.InitialStock);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldRaiseProductAddedEvent()
    {
        // Arrange
        var command = new AddProductCommand(
            "Test Product",
            "Test Description",
            99.99m,
            "EUR",
            10);

        Product? capturedProduct = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(p => capturedProduct = p)
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedProduct.Should().NotBeNull();
        capturedProduct!.DomainEvents.Should().HaveCount(1);
        var domainEvent = capturedProduct.DomainEvents.First() as ProductCatalog.Domain.Events.ProductAdded;
        domainEvent.Should().NotBeNull();
        domainEvent!.Name.Should().Be(command.Name);
        domainEvent.Description.Should().Be(command.Description);
        domainEvent.Price.Should().Be(command.PriceAmount);
        domainEvent.Currency.Should().Be(command.Currency);
        domainEvent.InitialStock.Should().Be(command.InitialStock);
    }

    [Theory]
    [InlineData("USD", 149.99)]
    [InlineData("GBP", 79.99)]
    [InlineData("JPY", 15000)]
    public async Task Handle_WithDifferentCurrencies_ShouldCreateCorrectPrice(string currency, decimal amount)
    {
        // Arrange
        var command = new AddProductCommand(
            "Test Product",
            "Test Description",
            amount,
            currency,
            5);

        Product? capturedProduct = null;
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(p => capturedProduct = p)
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedProduct.Should().NotBeNull();
        capturedProduct!.Price.Currency.Should().Be(currency);
        capturedProduct.Price.Amount.Should().Be(amount);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldPropagateException()
    {
        // Arrange
        var command = new AddProductCommand(
            "Test Product",
            "Test Description",
            99.99m,
            "EUR",
            10);

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await _handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Database error");
    }
}
