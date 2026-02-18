using System.ComponentModel.DataAnnotations;

namespace Mini_E_Commerce_API.DTO
{
    /// <summary>
    /// Product DTO for API responses
    /// Separates domain model from API contract
    /// </summary>
    public class ProductDto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 200 characters")]
        public string Name { get; set; } = null!;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO for creating new products
    /// Excludes read-only fields (Id, CreatedAt, UpdatedAt)
    /// </summary>
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 200 characters")]
        public string Name { get; set; } = null!;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Valid category ID is required")]
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// DTO for updating products
    /// All fields are optional to support PATCH operations
    /// </summary>
    public class UpdateProductDto
    {
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 200 characters")]
        public string? Name { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Valid category ID is required")]
        public int? CategoryId { get; set; }
    }
}
