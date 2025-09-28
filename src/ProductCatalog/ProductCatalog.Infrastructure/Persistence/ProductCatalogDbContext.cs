using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContext : DbContext
{
    public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();

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
