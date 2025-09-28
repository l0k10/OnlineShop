using ProductCatalog.Application.Features.AddProduct;

namespace ProductCatalog.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products");

        // Commands
        group.MapPost("/", AddProductEndpoint.HandleAsync)
            .WithName("AddProduct")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }
}
