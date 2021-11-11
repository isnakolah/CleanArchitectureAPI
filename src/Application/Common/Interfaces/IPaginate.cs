using Application.Common.Models;

namespace Application.Common.Interfaces;

/// <summary>
/// IPaginate interface
/// </summary>
public interface IPaginate
{
    /// <summary>
    /// Create Method creates the pagination response
    /// </summary>
    /// <typeparam name="In">Type of Queryable</typeparam>
    /// <typeparam name="Out">Type to be converted to(dto)</typeparam>
    /// <param name="source">Queryable object</param>
    /// <param name="filter">The pagination filters</param>
    /// <param name="route">The route of the controller</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <param name="queryParams">Optional query params for the Uri creation</param>
    /// <returns>PaginatedService response with the pagination details</returns>
    Task<PaginatedServiceResult<Out>> CreateAsync<In, Out>(
        IQueryable<In> source,
        PaginationFilter filter,
        CancellationToken cancellationToken,
        List<QueryParam> queryParams)
        where In : class where Out : class;

    /// <summary>
    /// Create Method creates the pagination response
    /// </summary>
    /// <typeparam name="In">Type of Queryable</typeparam>
    /// <typeparam name="Out">Type to be converted to(dto)</typeparam>
    /// <param name="source">Queryable object</param>
    /// <param name="filter">The pagination filters</param>
    /// <param name="route">The route of the controller</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <param name="queryParam">Optional query params for the Uri creation</param>
    /// <returns>PaginatedService response with the pagination details</returns>
    Task<PaginatedServiceResult<Out>> CreateAsync<In, Out>(
        IQueryable<In> source,
        PaginationFilter filter,
        CancellationToken cancellationToken,
        QueryParam queryParam = null)
        where In : class where Out : class;
}
