using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Events;

public record ProductBecameUnavailable(Guid ProductId) : IDomainEvent;
