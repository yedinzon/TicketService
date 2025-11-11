using Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class IQueryableExtensions
{
    /// <summary>
    /// Extension method to convert an IQueryable to a paged result.
    /// </summary>
    /// <typeparam name="T">The type of the items in the query.</typeparam>
    /// <param name="query">Queryable to paginate.</param>
    /// <param name="pageNumber">Number of the page to retrieve.</param>
    /// <param name="pageSize">Size of the page to retrieve.</param>
    /// <returns></returns>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        int totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>(items, pageNumber, pageSize, totalCount);
    }
}
