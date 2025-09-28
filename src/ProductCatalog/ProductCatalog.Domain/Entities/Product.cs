using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Events;
using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Domain.Entities;

public class Product : AggregateRoot
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public Price Price { get; private set; } = Price.Zero;

    public int Stock { get; private set; }

    public bool IsAvailable => Stock > 0;

    private Product()
    {
    }

    private Product(Guid id, string name, string description, Price price, int stock)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public static Product Create(string name, string description, Price price, int initialStock)
    {
        var product = new Product(Guid.NewGuid(), name, description, price, initialStock);
        product.AddDomainEvent(new ProductAdded(product.Id, name, description, price.Amount, price.Currency, initialStock));
        return product;
    }

    public void ChangePrice(Price newPrice)
    {
        var oldPrice = Price;
        Price = newPrice;
        AddDomainEvent(new PriceChanged(Id, oldPrice.Amount, newPrice.Amount, newPrice.Currency));
    }

    public void UpdateStock(int newStock)
    {
        var oldStock = Stock;
        Stock = newStock;
        AddDomainEvent(new StockUpdated(Id, oldStock, newStock));

        if (oldStock > 0 && newStock == 0)
        {
            AddDomainEvent(new ProductBecameUnavailable(Id));
        }
    }
}
