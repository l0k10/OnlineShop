using MediatR;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Repositories;
using ProductCatalog.Domain.ValueObjects;

namespace ProductCatalog.Application.Features.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public AddProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var price = new Price(request.PriceAmount, request.Currency);
        var product = Product.Create(
            request.Name,
            request.Description,
            price,
            request.InitialStock);

        await _repository.AddAsync(product);

        return product.Id;
    }
}
