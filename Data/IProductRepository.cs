using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Data
{
    /// <summary>
    /// Product-specific repository interface
    /// Extends generic repository with product-specific queries
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Get products by category ID with pagination
        /// </summary>
        Task<DTO.PagedResponse<Product>> GetByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Get products within price range
        /// </summary>
        Task<DTO.PagedResponse<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Search products by name (full-text search ready)
        /// </summary>
        Task<DTO.PagedResponse<Product>> SearchByNameAsync(string searchTerm, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Get product with category eager loaded
        /// </summary>
        Task<Product?> GetWithCategoryAsync(int id);
    }
}
