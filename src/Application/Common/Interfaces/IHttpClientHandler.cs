using Application.Common.Models;
using Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// IHttpClientHandler interface
    /// </summary>
    public interface IHttpClientHandler
    {
        /// <summary>
        /// Create a Request
        /// </summary>
        /// <typeparam name="TRequest">The type of request</typeparam>
        /// <typeparam name="TResult">The type of the result</typeparam>
        /// <param name="clientAPI">The name for the api</param>
        /// <param name="url">The endpoint of the application</param>
        /// <param name="cancellationToken">The cancellation token for the async process</param>
        /// <param name="method">Optional method type (Get/Post) default set to Get</param>
        /// <param name="requestEntity">The request entity</param>
        /// <returns></returns>
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(
            string clientAPI, 
            string url,
            CancellationToken cancellationToken,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null
            )
            where TResult : class where TRequest : class;
    }
}
