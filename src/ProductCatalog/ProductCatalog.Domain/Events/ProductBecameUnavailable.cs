using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

/// <summary>
/// Domain event that is raised when a product becomes unavailable (stock reaches zero).
/// </summary>
/// <param name="ProductId">The unique identifier of the product that became unavailable.</param>
public record ProductBecameUnavailable(Guid ProductId) : IDomainEvent;
