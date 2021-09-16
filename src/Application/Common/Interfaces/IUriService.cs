using Application.Common.Models;
using System;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// IUriService interface
    /// </summary>
    public interface IUriService
    {
        /// <summary>
        /// Getting the PageUri for pagination
        /// </summary>
        /// <param name="paginationFilter">Filter object with the filters for paginating</param>
        /// <param name="route">The current route of the controller</param>
        /// <returns>Created Uri with the queryParams</returns>
        public Uri GetPageUri(PaginationFilter paginationFilter, List<QueryParam> queryParams = null);

        /// <summary>
        /// Getting the PageUri for pagination
        /// </summary>
        /// <param name="paginationFilter">Filter object with the filters for paginating</param>
        /// <param name="route">The current route of the controller</param>
        /// <returns>Created Uri with the queryParams</returns>
        public Uri GetPageUri(PaginationFilter paginationFilter, QueryParam queryParam = null);

        /// <summary>
        /// Method for creating a Uri
        /// </summary>
        /// <param name="route">The route of the current controller</param>
        /// <param name="queryParams">The query parameters(key, value) to be added</param>
        /// <returns>A query parameter Uri</returns>
        public Uri GetUri(QueryParam queryParams);

        /// <summary>
        /// Get the Uri by appending it to the end of the route
        /// </summary>
        /// <param name="route">The route where the uri will be appended</param>
        /// <param name="ID">Id of the entity</param>
        /// <returns>Route uri with the Id appended</returns>
        public Uri GetIDUri(Guid ID);
    }
}
