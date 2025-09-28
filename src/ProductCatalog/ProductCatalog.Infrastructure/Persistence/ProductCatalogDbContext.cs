using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence;

/// <summary>
/// Database context for the Product Catalog, providing access to product entities.
/// </summary>
public class ProductCatalogDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductCatalogDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the products DbSet.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.Description)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(p => p.Stock)
                .IsRequired();

            // Value Object Price für SQLite
            entity.OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("PriceAmount")
                    .HasColumnType("decimal(18,2)");

                price.Property(p => p.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasMaxLength(3);
            });

            // Ignore Domain Events
            entity.Ignore(p => p.DomainEvents);
        });

        base.OnModelCreating(modelBuilder);
    }
}
