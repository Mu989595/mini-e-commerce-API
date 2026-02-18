using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Data
{
    /// <summary>
    /// Product Repository Implementation
    /// Provides product-specific data access operations
    /// 
    /// Features:
    /// - Category filtering
    /// - Price range filtering
    /// - Full-text search capability
    /// - Eager loading with Category
    /// </summary>
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<PagedResponse<Product>> GetByCategoryAsync(int categoryId, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalCount = await _dbSet.AsNoTracking()
                .CountAsync(p => p.CategoryId == categoryId);

            var data = await _dbSet
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Product>(data, totalCount, pageNumber, pageSize);
        }

        public async Task<PagedResponse<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalCount = await _dbSet.AsNoTracking()
                .CountAsync(p => p.Price >= minPrice && p.Price <= maxPrice);

            var data = await _dbSet
                .AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .Include(p => p.Category)
                .OrderBy(p => p.Price)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Product>(data, totalCount, pageNumber, pageSize);
        }

        public async Task<PagedResponse<Product>> SearchByNameAsync(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _dbSet.AsNoTracking()
                .Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));

            var totalCount = await query.CountAsync();

            var data = await query
                .Include(p => p.Category)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Product>(data, totalCount, pageNumber, pageSize);
        }

        public async Task<Product?> GetWithCategoryAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
