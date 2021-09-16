using Application.Common.Models;
using MediatR;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Interface Wrapper for the IRequest, wraps with the service result
    /// </summary>
    /// <typeparam name="T">Type of the return data</typeparam>
    public interface IRequestWrapper<T> : IRequest<ServiceResult<T>>
    {
    }

    /// <summary>
    /// Interface wrapper for the IRequestHandler for IRequestWrapper
    /// </summary>
    /// <typeparam name="TIn">IRequestWrapper instance</typeparam>
    /// <typeparam name="TOut">Type of the return</typeparam>
    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut>
    {
    }

    /// <summary>
    /// Interface Wrapper for the IRequest, wraps with the paginated service result
    /// </summary>
    /// <typeparam name="T">Type of the data in the list to be returned</typeparam>
    public interface IRequestPaginatedWrapper<T> : IRequest<PaginatedServiceResult<T>>
    {
    }

    /// <summary>
    /// Interface Wrapper for the IRequestHandler for the IRequestPaginatedWrapper
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface IRequestHandlerPaginatedWrapper<TIn, TOut> : IRequestHandler<TIn, PaginatedServiceResult<TOut>> where TIn : IRequestPaginatedWrapper<TOut>
    {
    }
}
