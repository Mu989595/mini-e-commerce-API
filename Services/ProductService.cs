using Mini_E_Commerce_API.Data;
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Services
{
    /// <summary>
    /// Product Service Implementation
    /// 
    /// Responsibilities:
    /// - Business logic for product operations
    /// - Data validation
    /// - DTO conversion and mapping
    /// - Exception handling and error responses
    /// - Coordination between repository and API layer
    /// 
    /// Scalability Features:
    /// - Pagination support
    /// - Query filtering (category, price range, search)
    /// - Async operations for non-blocking I/O
    /// - Efficient database queries with Include()
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository,
            IRepository<Category> categoryRepository,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<PagedResponse<ProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching products - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);

                var pagedProducts = await _productRepository.GetAllAsync(pageNumber, pageSize);
                var productDtos = MapProductsToDto(pagedProducts.Data);

                return new PagedResponse<ProductDto>(
                    productDtos,
                    pagedProducts.TotalCount,
                    pageNumber,
                    pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all products");
                throw;
            }
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching product with ID: {ProductId}", id);

                var product = await _productRepository.GetWithCategoryAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                    return null;
                }

                return MapProductToDto(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product {ProductId}", id);
                throw;
            }
        }

        public async Task<PagedResponse<ProductDto>> GetByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Validate category exists
                var categoryExists = await _categoryRepository.AnyAsync(c => c.Id == categoryId);
                if (!categoryExists)
                {
                    _logger.LogWarning("Category with ID {CategoryId} not found", categoryId);
                    return new PagedResponse<ProductDto>(new List<ProductDto>(), 0, pageNumber, pageSize);
                }

                _logger.LogInformation("Fetching products for category {CategoryId}", categoryId);
                var pagedProducts = await _productRepository.GetByCategoryAsync(categoryId, pageNumber, pageSize);
                var productDtos = MapProductsToDto(pagedProducts.Data);

                return new PagedResponse<ProductDto>(
                    productDtos,
                    pagedProducts.TotalCount,
                    pageNumber,
                    pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products for category {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<PagedResponse<ProductDto>> SearchAsync(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    _logger.LogWarning("Search term is empty");
                    return new PagedResponse<ProductDto>(new List<ProductDto>(), 0, pageNumber, pageSize);
                }

                _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);
                var pagedProducts = await _productRepository.SearchByNameAsync(searchTerm, pageNumber, pageSize);
                var productDtos = MapProductsToDto(pagedProducts.Data);

                return new PagedResponse<ProductDto>(
                    productDtos,
                    pagedProducts.TotalCount,
                    pageNumber,
                    pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products with term: {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<PagedResponse<ProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                {
                    _logger.LogWarning("Invalid price range: min={MinPrice}, max={MaxPrice}", minPrice, maxPrice);
                    return new PagedResponse<ProductDto>(new List<ProductDto>(), 0, pageNumber, pageSize);
                }

                _logger.LogInformation("Fetching products in price range: {MinPrice} - {MaxPrice}", minPrice, maxPrice);
                var pagedProducts = await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice, pageNumber, pageSize);
                var productDtos = MapProductsToDto(pagedProducts.Data);

                return new PagedResponse<ProductDto>(
                    productDtos,
                    pagedProducts.TotalCount,
                    pageNumber,
                    pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products in price range");
                throw;
            }
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createDto)
        {
            try
            {
                // Validate category exists
                var categoryExists = await _categoryRepository.AnyAsync(c => c.Id == createDto.CategoryId);
                if (!categoryExists)
                {
                    throw new InvalidOperationException($"Category with ID {createDto.CategoryId} does not exist");
                }

                var product = new Product
                {
                    Name = createDto.Name.Trim(),
                    Price = createDto.Price,
                    CategoryId = createDto.CategoryId,
                    CreatedAt = DateTime.UtcNow
                };

                await _productRepository.AddAsync(product);
                await _productRepository.SaveChangesAsync();

                _logger.LogInformation("Product created successfully with ID: {ProductId}", product.Id);

                return MapProductToDto(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateDto)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }

                // Only update fields that are provided
                if (!string.IsNullOrWhiteSpace(updateDto.Name))
                {
                    product.Name = updateDto.Name.Trim();
                }

                if (updateDto.Price.HasValue && updateDto.Price > 0)
                {
                    product.Price = updateDto.Price.Value;
                }

                if (updateDto.CategoryId.HasValue && updateDto.CategoryId > 0)
                {
                    var categoryExists = await _categoryRepository.AnyAsync(c => c.Id == updateDto.CategoryId);
                    if (!categoryExists)
                    {
                        throw new InvalidOperationException($"Category with ID {updateDto.CategoryId} does not exist");
                    }
                    product.CategoryId = updateDto.CategoryId.Value;
                }

                product.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();

                _logger.LogInformation("Product {ProductId} updated successfully", id);

                return MapProductToDto(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }

                await _productRepository.DeleteAsync(id);
                await _productRepository.SaveChangesAsync();

                _logger.LogInformation("Product {ProductId} deleted successfully", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {ProductId}", id);
                throw;
            }
        }

        // ============ HELPER METHODS ============

        private ProductDto MapProductToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private IEnumerable<ProductDto> MapProductsToDto(IEnumerable<Product> products)
        {
            return products.Select(MapProductToDto);
        }
    }
}
