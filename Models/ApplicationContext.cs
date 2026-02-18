using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Models
{
    /// <summary>
    /// Application DbContext
    /// Manages all entity configurations and database interactions
    /// 
    /// Features:
    /// - Identity integration for users and roles
    /// - Fluent API configurations for constraints
    /// - Cascade delete policies
    /// - Audit field conventions
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============ CATEGORY CONFIGURATION ============
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                entity.Property(c => c.Description)
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");

                // Index for faster lookups
                entity.HasIndex(c => c.Name)
                    .IsUnique()
                    .HasDatabaseName("IX_Category_Name");

                entity.Property(c => c.CreatedAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(c => c.UpdatedAt)
                    .HasColumnType("datetime2");

                // Seed default categories
                entity.HasData(
                    new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and gadgets" },
                    new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion items" },
                    new Category { Id = 3, Name = "Books", Description = "Books and educational materials" },
                    new Category { Id = 4, Name = "Home & Kitchen", Description = "Home and kitchen appliances" },
                    new Category { Id = 5, Name = "Sports", Description = "Sports and outdoor equipment" }
                );
            });

            // ============ PRODUCT CONFIGURATION ============
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("nvarchar(200)");

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.CategoryId)
                    .IsRequired();

                // Indexes for performance
                entity.HasIndex(p => p.CategoryId)
                    .HasDatabaseName("IX_Product_CategoryId");

                entity.HasIndex(p => p.Name)
                    .HasDatabaseName("IX_Product_Name");

                // Foreign key configuration with cascade delete
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p => p.CreatedAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(p => p.UpdatedAt)
                    .HasColumnType("datetime2");

                // Concurrency token
                entity.Property(p => p.RowVersion)
                    .IsRowVersion()
                    .HasColumnType("rowversion");
            });

            // ============ USER/IDENTITY CONFIGURATION ============
            // Customize ASP.NET Core Identity tables if needed
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("AspNetRoles");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("AspNetUserClaims");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("AspNetRoleClaims");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("AspNetUserTokens");
        }
    }
}
