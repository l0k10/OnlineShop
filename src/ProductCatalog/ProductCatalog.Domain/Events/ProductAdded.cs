using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

public record ProductAdded(
Guid ProductId,
string Name,
string Description,
decimal Price,
string Currency,
int InitialStock) : IDomainEvent;
