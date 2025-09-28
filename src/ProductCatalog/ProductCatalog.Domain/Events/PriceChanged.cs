using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

/// <summary>
/// Domain event that is raised when a product's price is changed.
/// </summary>
/// <param name="ProductId">The unique identifier of the product.</param>
/// <param name="OldPrice">The previous price amount.</param>
/// <param name="NewPrice">The new price amount.</param>
/// <param name="Currency">The currency code of the price.</param>
public record PriceChanged(
Guid ProductId,
decimal OldPrice,
decimal NewPrice,
string Currency) : IDomainEvent;
