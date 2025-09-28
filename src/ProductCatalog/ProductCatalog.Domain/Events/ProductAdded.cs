using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

/// <summary>
/// Domain event that is raised when a new product is added to the catalog.
/// </summary>
/// <param name="ProductId">The unique identifier of the added product.</param>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The description of the product.</param>
/// <param name="Price">The price amount of the product.</param>
/// <param name="Currency">The currency code of the product price.</param>
/// <param name="InitialStock">The initial stock quantity of the product.</param>
public record ProductAdded(
Guid ProductId,
string Name,
string Description,
decimal Price,
string Currency,
int InitialStock) : IDomainEvent;
