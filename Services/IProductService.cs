using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Services
{
    /// <summary>
    /// Product Service Interface
    /// Encapsulates all product business logic operations
    /// 
    /// Advantages:
    /// - Separates business logic from controllers
    /// - Enables unit testing with mock repository
    /// - Centralizes product-related validations
    /// - Provides consistent error handling
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all products with pagination
        /// </summary>
        Task<PagedResponse<ProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Get product by ID with related data
        /// </summary>
        Task<ProductDto?> GetByIdAsync(int id);

        /// <summary>
        /// Get products by category
        /// </summary>
        Task<PagedResponse<ProductDto>> GetByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Search products by name
        /// </summary>
        Task<PagedResponse<ProductDto>> SearchAsync(string searchTerm, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Get products within price range
        /// </summary>
        Task<PagedResponse<ProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Create new product with validation
        /// </summary>
        Task<ProductDto> CreateAsync(CreateProductDto createDto);

        /// <summary>
        /// Update existing product
        /// </summary>
        Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateDto);

        /// <summary>
        /// Delete product
        /// </summary>
        Task DeleteAsync(int id);
    }
}
