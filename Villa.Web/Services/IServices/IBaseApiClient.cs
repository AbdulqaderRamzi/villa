using System;

namespace Villa.Web.Services.IServices;

public interface IBaseApiClient
{
    Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod httpMethod, string endpoint, TRequest? data = default);
}
