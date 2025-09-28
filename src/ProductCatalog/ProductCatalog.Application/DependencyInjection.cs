using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Common.Behaviors;

namespace ProductCatalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Validation
        services.AddValidatorsFromAssembly(assembly);

        // Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
