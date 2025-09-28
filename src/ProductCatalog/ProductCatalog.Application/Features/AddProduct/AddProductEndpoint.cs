using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Application.Features.AddProduct;

public static class AddProductEndpoint
{
    public static async Task<object> HandleAsync(
        [FromBody] AddProductCommand command,
        IMediator mediator)
    {
        var productId = await mediator.Send(command);
        return new { Id = productId };
    }
}
