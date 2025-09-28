using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<IEnumerable<Product>> GetAvailableAsync();

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Guid id);
}
