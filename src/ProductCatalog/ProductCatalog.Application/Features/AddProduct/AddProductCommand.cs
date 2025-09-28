using MediatR;

namespace ProductCatalog.Application.Features.AddProduct;

public record AddProductCommand(
string Name,
string Description,
decimal PriceAmount,
string Currency,
int InitialStock) : IRequest<Guid>;
