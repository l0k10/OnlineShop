using MediatR;

namespace ProductCatalog.Application.Features.ChangePrice;

/// <summary>
/// Command to change the price of an existing product.
/// </summary>
/// <param name="ProductId">The unique identifier of the product.</param>
/// <param name="NewPriceAmount">The new price amount.</param>
/// <param name="Currency">The currency code.</param>
public record ChangePriceCommand(
    Guid ProductId,
    decimal NewPriceAmount,
    string Currency) : IRequest;