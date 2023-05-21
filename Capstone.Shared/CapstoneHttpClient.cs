using Microsoft.Extensions.DependencyInjection;

namespace Capstone.Shared;

public static class CapstoneHttpClient
{
    public static void AddCapstoneHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpService, HttpService>();
    }
}