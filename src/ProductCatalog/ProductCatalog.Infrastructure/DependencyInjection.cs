using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Domain.Repositories;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Persistence.Repositories;

namespace ProductCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // SQLite Database
        services.AddDbContext<ProductCatalogDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=productcatalog.db"));

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
