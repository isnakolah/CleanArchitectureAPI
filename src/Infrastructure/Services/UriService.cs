using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class UriService : IUriService
    {
        private const string QueryPageNumber = "pageNumber";
        private const string QueryPageSize = "pageSize";

        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(PaginationFilter filter, QueryParam queryParam)
        {
            return GetPageUri(filter, new List<QueryParam> { queryParam });
        }

        public Uri GetPageUri(PaginationFilter filter, List<QueryParam> queryParams)
        {
            var endpointUri = _baseUri;

            var toAddQueryParams = new List<QueryParam>
            {
                new QueryParam(QueryPageNumber, filter.PageNumber.ToString()),
                new QueryParam(QueryPageSize, filter.PageSize.ToString()),
            };

            // Append the optionalQueryParams
            if (queryParams is not null)
                toAddQueryParams.AddRange(queryParams);

            AddQueryParameter(ref endpointUri, toAddQueryParams);

            return new Uri(endpointUri);
        }

        public Uri GetUri(QueryParam queryParams)
        {
            var endpointUri = _baseUri;

            AddQueryParameter(ref endpointUri, new List<QueryParam> { queryParams });

            return new Uri(endpointUri);
        }

        public Uri GetIDUri(Guid ID)
        {
            var endpointUri = _baseUri;

            AddTrailingSlashIfEndpointDoesNotHave(ref endpointUri);

            var createdUri = endpointUri + ID.ToString();

            return new Uri(createdUri);
        }

        private static void AddTrailingSlashIfEndpointDoesNotHave(ref string endpointUri)
        {
            if (!endpointUri.EndsWith("/"))
                endpointUri += "/";
        }

        /// <summary>
        /// Method to create a query uri
        /// </summary>
        /// <param name="currentUri">Uri for the query parameters to be added</param>
        /// <param name="queryKey">The parameter key to be added</param>
        /// <param name="queryValue">The parameter value to be added</param>
        /// <returns></returns>
        internal static void AddQueryParameter(ref string endpointUri, List<QueryParam> queryParams)
        {
            foreach (var queryParam in queryParams)
            {
                endpointUri = QueryHelpers.AddQueryString(
                    endpointUri,
                    queryParam.QueryKey,
                    queryParam.QueryValue);
            }
        }
    }
}
