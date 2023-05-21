using Capstone.Shared;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.EndUser.GraphQl;

public class Query
{
    private readonly string? baseUrl;

    public Query(IConfiguration configuration)
    {
        baseUrl = configuration["FlightApiURL"];
    }

    public string Hello() => "Hello, GraphQL";

    public async Task<List<BookingOutput>> GetBookings([FromServices] IHttpService httpService, string emailId)
    {
        var url = new Uri(new Uri(baseUrl), $"booking/history/{emailId}");
        var response =
            await httpService.Get<List<BookingOutput>>(url);

        return response.Result;
    }
}