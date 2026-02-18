using System.Linq.Expressions;
using Mini_E_Commerce_API.DTO;

namespace Mini_E_Commerce_API.Data
{
    /// <summary>
    /// Generic Repository Interface
    /// Provides common CRUD operations and query methods
    /// Benefits: Decouples data access, enables unit testing, standardizes operations
    /// </summary>
    public interface IRepository<T> where T : class
    {
        // ============ READ OPERATIONS ============
        
        /// <summary>
        /// Get all entities with optional pagination
        /// </summary>
        Task<PagedResponse<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        
        /// <summary>
        /// Get entity by ID with eager loading of related entities
        /// </summary>
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        
        /// <summary>
        /// Get first entity matching the predicate
        /// </summary>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        
        /// <summary>
        /// Get all entities matching the predicate with pagination
        /// </summary>
        Task<PagedResponse<T>> FindAsync(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10);
        
        /// <summary>
        /// Count total entities
        /// </summary>
        Task<int> CountAsync();
        
        /// <summary>
        /// Check if any entity matches the predicate
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // ============ WRITE OPERATIONS ============
        
        /// <summary>
        /// Add new entity
        /// </summary>
        Task AddAsync(T entity);
        
        /// <summary>
        /// Add multiple entities in batch
        /// </summary>
        Task AddRangeAsync(IEnumerable<T> entities);
        
        /// <summary>
        /// Update existing entity
        /// </summary>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Delete entity by ID
        /// </summary>
        Task DeleteAsync(int id);
        
        /// <summary>
        /// Delete specific entity
        /// </summary>
        Task DeleteAsync(T entity);
        
        /// <summary>
        /// Delete multiple entities
        /// </summary>
        Task DeleteRangeAsync(IEnumerable<T> entities);

        // ============ SAVE OPERATIONS ============
        
        /// <summary>
        /// Save all changes to database
        /// </summary>
        Task SaveChangesAsync();
    }
}
