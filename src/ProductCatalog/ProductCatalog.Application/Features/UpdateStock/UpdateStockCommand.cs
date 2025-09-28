using MediatR;

namespace ProductCatalog.Application.Features.UpdateStock;

/// <summary>
/// Command to update the stock quantity of an existing product.
/// </summary>
/// <param name="ProductId">The unique identifier of the product.</param>
/// <param name="NewStock">The new stock quantity.</param>
public record UpdateStockCommand(
    Guid ProductId,
    int NewStock) : IRequest;