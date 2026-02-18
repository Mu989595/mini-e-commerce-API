using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Services;

namespace Mini_E_Commerce_API.Controllers
{
    /// <summary>
    /// Product API Controller
    /// Handles all product-related HTTP requests
    /// 
    /// Architecture:
    /// - Controller receives requests and delegates to service layer
    /// - Service handles business logic and data validation
    /// - Repository handles database operations
    /// - DTOs provide API contract separation from domain models
    /// 
    /// Scalability Features:
    /// - Pagination for large datasets
    /// - Filtering by category, price range
    /// - Full-text search capability
    /// - Proper HTTP status codes (201, 204, 400, 404, 500)
    /// - Async operations for non-blocking I/O
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductControllerV2 : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductControllerV2> _logger;

        public ProductControllerV2(IProductService productService, ILogger<ProductControllerV2> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get all products with pagination
        /// GET: api/products?pageNumber=1&pageSize=10
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _productService.GetAllAsync(pageNumber, pageSize);
                return Ok(new
                {
                    isSuccess = true,
                    message = "Products retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while retrieving products"
                });
            }
        }

        /// <summary>
        /// Get product by ID
        /// GET: api/products/{id}
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        isSuccess = false,
                        message = $"Product with ID {id} not found"
                    });
                }

                return Ok(new
                {
                    isSuccess = true,
                    message = "Product retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product {ProductId}", id);
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while retrieving the product"
                });
            }
        }

        /// <summary>
        /// Get products by category
        /// GET: api/products/category/{categoryId}?pageNumber=1&pageSize=10
        /// </summary>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _productService.GetByCategoryAsync(categoryId, pageNumber, pageSize);
                return Ok(new
                {
                    isSuccess = true,
                    message = "Products retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products for category {CategoryId}", categoryId);
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while retrieving products"
                });
            }
        }

        /// <summary>
        /// Search products by name
        /// GET: api/products/search?term=laptop&pageNumber=1&pageSize=10
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Search([FromQuery] string term, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    return BadRequest(new
                    {
                        isSuccess = false,
                        message = "Search term cannot be empty"
                    });
                }

                var result = await _productService.SearchAsync(term, pageNumber, pageSize);
                return Ok(new
                {
                    isSuccess = true,
                    message = "Search results retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products with term: {SearchTerm}", term);
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while searching products"
                });
            }
        }

        /// <summary>
        /// Get products by price range
        /// GET: api/products/price?minPrice=100&maxPrice=5000&pageNumber=1&pageSize=10
        /// </summary>
        [HttpGet("price")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByPriceRange(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
                {
                    return BadRequest(new
                    {
                        isSuccess = false,
                        message = "Invalid price range: min must be less than max and both must be positive"
                    });
                }

                var result = await _productService.GetByPriceRangeAsync(minPrice, maxPrice, pageNumber, pageSize);
                return Ok(new
                {
                    isSuccess = true,
                    message = "Products retrieved successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products by price range");
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while retrieving products"
                });
            }
        }

        /// <summary>
        /// Create new product
        /// POST: api/products
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        isSuccess = false,
                        message = "Invalid product data",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                var result = await _productService.CreateAsync(createDto);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, new
                {
                    isSuccess = true,
                    message = "Product created successfully",
                    data = result
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error while creating product");
                return BadRequest(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while creating the product"
                });
            }
        }

        /// <summary>
        /// Update product
        /// PUT: api/products/{id}
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        isSuccess = false,
                        message = "Invalid product data",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                var result = await _productService.UpdateAsync(id, updateDto);

                return Ok(new
                {
                    isSuccess = true,
                    message = "Product updated successfully",
                    data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product not found: {ProductId}", id);
                return NotFound(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error while updating product {ProductId}", id);
                return BadRequest(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", id);
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while updating the product"
                });
            }
        }

        /// <summary>
        /// Delete product
        /// DELETE: api/products/{id}
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Product not found: {ProductId}", id);
                return NotFound(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {ProductId}", id);
                return StatusCode(500, new
                {
                    isSuccess = false,
                    message = "An error occurred while deleting the product"
                });
            }
        }
    }
}
