namespace Capstone.Shared;

public interface IHttpService
{
    Task<ApiResult<TResponse>> Get<TResponse>(Uri url);
    Task<ApiResult<TResponse>> Post<T, TResponse>(Uri url, T data);

    Task<ApiResult<TResponse>> Put<T, TResponse>(Uri url, T? data);

    Task<ApiResult<TResponse>> Delete<TResponse>(Uri url);
}