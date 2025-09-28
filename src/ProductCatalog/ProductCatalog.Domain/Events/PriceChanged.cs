using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

public record PriceChanged(
Guid ProductId,
decimal OldPrice,
decimal NewPrice,
string Currency) : IDomainEvent;
