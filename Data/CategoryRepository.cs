using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Data
{
    /// <summary>
    /// Category Repository Implementation
    /// Provides category-specific data access operations
    /// </summary>
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<Category?> GetWithProductsAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
