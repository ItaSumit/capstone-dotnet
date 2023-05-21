using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Capstone.Shared;

public class HttpService : IHttpService
{
    private readonly HttpClient _client;

    public HttpService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult<TResponse>> Get<TResponse>(Uri url)
    {
        var response = await _client.GetAsync(url);

        var result = await response.Content.ReadAsAsync<TResponse>();

        return new ApiResult<TResponse>(result);
    }

    public async Task<ApiResult<TResponse>> Post<T, TResponse>(Uri url, T data)
    {
        var response =
            await _client.PostAsJsonAsync(url, data);

        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentLength > 0)
            {
                var success = await response.Content.ReadAsAsync<TResponse>();
                return new ApiResult<TResponse>(success);
            }
            else
            {
                return new ApiResult<TResponse>();
            }
        }

        //return response;
        return new ApiResult<TResponse>((int)response.StatusCode);
    }

    public async Task<ApiResult<TResponse>> Put<T, TResponse>(Uri url, T? data)
    {
        var response =
            await _client.PutAsJsonAsync(url, data);

        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentLength > 0)
            {
                var success = await response.Content.ReadAsAsync<TResponse>();
                return new ApiResult<TResponse>(success);
            }
            else
            {
                return new ApiResult<TResponse>();
            }
        }

        //return response;
        return new ApiResult<TResponse>((int)response.StatusCode);
    }
    
    public async Task<ApiResult<TResponse>> Delete<TResponse>(Uri url)
    {
        var response =
            await _client.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentLength > 0)
            {
                var success = await response.Content.ReadAsAsync<TResponse>();
                return new ApiResult<TResponse>(success);
            }
            else
            {
                return new ApiResult<TResponse>();
            }
        }

        //return response;
        return new ApiResult<TResponse>((int)response.StatusCode);
    }
}

public class ApiResult<T>
{
    public bool success { get; set; }

    public int ErrorCode { get; set; }

    public T Result { get; set; }

    // public string ErrorMessage { get; set; }


    public ApiResult()
    {
        success = true;
    }

    public ApiResult(T result)
    {
        success = true;
        Result = result;
    }

    public ApiResult(int errorCode)
    {
        success = false;
        ErrorCode = errorCode;
        //ErrorMessage = errorMessage;
    }
}