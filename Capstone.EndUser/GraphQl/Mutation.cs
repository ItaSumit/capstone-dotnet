using Capstone.Shared;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.EndUser.GraphQl;

public class Mutation
{
    private readonly string? baseUrl;

    public Mutation(IConfiguration configuration)
    {
        baseUrl = configuration["FlightApiURL"];
    }

    public async Task<PNRPayload> BookFlight(BookingInput bookingInput, [FromServices] IHttpService httpService)
    {
        var url = new Uri(new Uri(baseUrl), "booking/book-flight");
        var response =
            await httpService.Post<BookingInput, string>(url, bookingInput);

        if (response.success)
        {
            return new PNRPayload(response.Result);
        }
        else
        {
            return new PNRPayload(null, response.ErrorCode.ToString());
        }
    }
}

public record PNRPayload(string? pnr, string? error = null) : Payload(error);

public record Payload(string? error);