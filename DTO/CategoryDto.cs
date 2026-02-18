using System.ComponentModel.DataAnnotations;

namespace Mini_E_Commerce_API.DTO
{
    /// <summary>
    /// Category DTO for API responses
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;
        
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO for creating categories
    /// </summary>
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string Name { get; set; } = null!;
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
    }

    /// <summary>
    /// DTO for updating categories
    /// </summary>
    public class UpdateCategoryDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string? Name { get; set; }
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
    }
}
