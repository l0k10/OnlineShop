using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

/// <summary>
/// Domain event that is raised when a product's stock quantity is updated.
/// </summary>
/// <param name="ProductId">The unique identifier of the product.</param>
/// <param name="OldStock">The previous stock quantity.</param>
/// <param name="NewStock">The new stock quantity.</param>
public record StockUpdated(
Guid ProductId,
int OldStock,
int NewStock) : IDomainEvent;
