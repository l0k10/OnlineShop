﻿namespace ProductCatalog.Domain.Common;

public interface IDomainEvent
{
    Guid Id => Guid.NewGuid();
    DateTime OccurredOn => DateTime.UtcNow;
}
