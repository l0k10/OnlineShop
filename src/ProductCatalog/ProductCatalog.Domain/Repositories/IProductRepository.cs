using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Repositories;

/// <summary>
/// Repository interface for managing product entities.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Gets a product by its unique identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <returns>The product if found, otherwise null.</returns>
    Task<Product?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets all products in the catalog.
    /// </summary>
    /// <returns>A collection of all products.</returns>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary>
    /// Gets all available products (with stock > 0).
    /// </summary>
    /// <returns>A collection of available products.</returns>
    Task<IEnumerable<Product>> GetAvailableAsync();

    /// <summary>
    /// Adds a new product to the catalog.
    /// </summary>
    /// <param name="product">The product to add.</param>
    Task AddAsync(Product product);

    /// <summary>
    /// Updates an existing product in the catalog.
    /// </summary>
    /// <param name="product">The product to update.</param>
    Task UpdateAsync(Product product);

    /// <summary>
    /// Deletes a product from the catalog.
    /// </summary>
    /// <param name="id">The identifier of the product to delete.</param>
    Task DeleteAsync(Guid id);
}
