using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Infrastructure.Services.Handlers;

public class HttpClientHandler : IHttpClientHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<IHttpClientHandler> _logger;

    public HttpClientHandler(IHttpClientFactory httpClientFactory, ILogger<IHttpClientHandler> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientAPI, string url, CancellationToken cancellationToken, MethodType method = MethodType.Get, TRequest requestEntity = null)
        where TRequest : class
        where TResult : class
    {
        var httpClient = _httpClientFactory.CreateClient();

        var requestName = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("HttpClient Request: {RequestName} {@Request}", requestName, requestEntity);

            var response = method switch
            {
                MethodType.Get => await httpClient.GetAsync(url, cancellationToken),
                MethodType.Post => await httpClient.PostAsJsonAsync(url, requestEntity, cancellationToken),
                _ => null
            };

            if (response is not null && response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken);
                return ServiceResult.Success(data);
            }

            if (response is null)
                return new ServiceResult<TResult>(ServiceError.ServiceProvider);

            var message = await response.Content.ReadAsStringAsync(cancellationToken);

            var error = new ServiceError(message, (int)response.StatusCode);

            return ServiceResult.Failed<TResult>(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HttpClient Request: Unhandled Exception for Request {RequestName} {@Request}", requestName, requestEntity);
            return ServiceResult.Failed<TResult>(ServiceError.CustomMessage(ex.ToString()));
        }
    }
}
