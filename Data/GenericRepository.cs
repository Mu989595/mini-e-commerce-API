using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce_API.DTO;
using Mini_E_Commerce_API.Models;

namespace Mini_E_Commerce_API.Data
{
    /// <summary>
    /// Generic Repository Implementation
    /// Provides base CRUD functionality for all entities
    /// 
    /// Design Patterns Used:
    /// - Repository Pattern: Abstracts data access logic
    /// - Generic Programming: Reusable across all entity types
    /// - Async/Await: Non-blocking database operations
    /// - Query Optimization: AsNoTracking() for read-only operations
    /// </summary>
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ============ READ OPERATIONS ============

        public virtual async Task<PagedResponse<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100); // Max 100 items per page

            var totalCount = await _dbSet.AsNoTracking().CountAsync();
            var data = await _dbSet
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<T>(data, totalCount, pageNumber, pageSize);
        }

        public virtual async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            // Eager load related entities
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<PagedResponse<T>> FindAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalCount = await _dbSet.AsNoTracking().CountAsync(predicate);
            var data = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<T>(data, totalCount, pageNumber, pageSize);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }

        // ============ WRITE OPERATIONS ============

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        // ============ SAVE OPERATIONS ============

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
