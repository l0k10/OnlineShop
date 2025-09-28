using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

public record StockUpdated(
Guid ProductId,
int OldStock,
int NewStock) : IDomainEvent;
