using MediatR;
using ProductCatalog.Application.Features.AddProduct;

namespace ProductCatalog.Api.Extensions;

/// <summary>
/// Extension methods for mapping API endpoints.
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Maps all product-related endpoints to the web application.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    public static void MapProductEndpoints(this WebApplication app)
    {
        // AddProduct Endpoint - MINIMAL
        app.MapPost("/api/products", async (AddProductCommand command, IMediator mediator) =>
        {
            var productId = await mediator.Send(command);
            return Results.Created($"/api/products/{productId}", new { Id = productId });
        })
        .WithName("AddProduct")
        .WithTags("Products");
    }
}
