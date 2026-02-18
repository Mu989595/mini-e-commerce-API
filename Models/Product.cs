using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_E_Commerce_API.Models
{
    /// <summary>
    /// Product Entity
    /// Represents a product in the e-commerce system
    /// 
    /// Constraints:
    /// - Name: Required, max 200 chars, indexed
    /// - Price: Decimal (not int), must be positive
    /// - CategoryId: Foreign key with cascade delete
    /// - Audit fields for tracking changes
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(200, ErrorMessage = "Product name cannot exceed 200 characters")]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Product price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }

        // ============ Audit Fields ============
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

        // ============ Concurrency Control (Optional) ============
        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}

