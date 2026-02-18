using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_E_Commerce_API.Models
{
    /// <summary>
    /// Category Entity
    /// Represents a product category in the e-commerce system
    /// 
    /// Constraints:
    /// - Name: Required, unique, max 100 chars
    /// - Description: Optional
    /// - Products: Navigation collection with cascade delete
    /// </summary>
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Column(TypeName = "nvarchar(500)")]
        public string? Description { get; set; }

        // ============ Navigation Property ============
        /// <summary>
        /// Collection of products in this category
        /// Cascade delete ensures products are deleted when category is deleted
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        // ============ Audit Fields ============
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }
    }
}

