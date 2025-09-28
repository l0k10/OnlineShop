using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Repositories;

namespace ProductCatalog.Infrastructure.Persistence.Repositories;

/// <summary>
/// Entity Framework implementation of the product repository.
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ProductCatalogDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ProductRepository(ProductCatalogDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a product by its unique identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <returns>The product if found, otherwise null.</returns>
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    /// <summary>
    /// Gets all products in the catalog.
    /// </summary>
    /// <returns>A collection of all products.</returns>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Gets all available products (with stock > 0).
    /// </summary>
    /// <returns>A collection of available products.</returns>
    public async Task<IEnumerable<Product>> GetAvailableAsync()
    {
        return await _context.Products
            .Where(p => p.Stock > 0)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new product to the catalog.
    /// </summary>
    /// <param name="product">The product to add.</param>
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing product in the catalog.
    /// </summary>
    /// <param name="product">The product to update.</param>
    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a product from the catalog.
    /// </summary>
    /// <param name="id">The identifier of the product to delete.</param>
    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
