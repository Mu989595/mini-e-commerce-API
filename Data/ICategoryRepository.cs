using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Data
{
    /// <summary>
    /// Category Repository Interface
    /// Extends generic repository with category-specific queries
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Get category by name
        /// </summary>
        Task<Category?> GetByNameAsync(string name);

        /// <summary>
        /// Get category with products
        /// </summary>
        Task<Category?> GetWithProductsAsync(int id);
    }
}
