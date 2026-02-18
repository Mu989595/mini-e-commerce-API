namespace Mini_E_Commerce_API.DTO
{
    /// <summary>
    /// Pagination Parameters for API queries
    /// Supports offset-based pagination with sorting
    /// </summary>
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public bool Descending { get; set; } = false;
    }

    /// <summary>
    /// Generic paged response wrapper
    /// Encapsulates paginated data with metadata
    /// Usage: Provides consistent API contracts for paginated endpoints
    /// </summary>
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public PagedResponse() { }

        /// <summary>
        /// Create paged response from query results
        /// </summary>
        public PagedResponse(IEnumerable<T> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            HasNextPage = pageNumber < TotalPages;
            HasPreviousPage = pageNumber > 1;
        }
    }
}
