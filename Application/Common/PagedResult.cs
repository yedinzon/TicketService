namespace Application.Common;

/// <summary>
/// Defines a paged result set.
/// </summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Creates a new instance of <see cref="PagedResult{T}"/>.
    /// </summary>
    /// <param name="items">The items in the current page.</param>
    /// <param name="pageNumber">Number of the current page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="totalCount">Number of total items.</param>
    public PagedResult(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public IReadOnlyList<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}
