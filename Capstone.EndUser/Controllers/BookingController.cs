using Capstone.Shared;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.EndUser.Controllers;

[ApiController]
[Route("user/api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IHttpService _httpService;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;

    public BookingController(IHttpService httpService, IConfiguration configuration)
    {
        _httpService = httpService;
        _configuration = configuration;
        _baseUrl = configuration["FlightApiURL"];
    }

    [HttpPost]
    [Route("book-flight")]
    public async Task<ActionResult> BookFlight(BookingInput bookingInput)
    {
        var url = new Uri(new Uri(_baseUrl), "booking/book-flight");
        var response =
            await _httpService.Post<BookingInput, string>(url, bookingInput);

        if (response.success)
        {
            return Ok(response.Result);
        }
        else
        {
            return BadRequest(response.ErrorCode);
        }

        return Ok(response.Result);
    }

    [HttpGet]
    [Route("history/{emailId}")]
    public async Task<ActionResult> GetUserBooking(string emailId)
    {
        var url = new Uri(new Uri(_baseUrl), $"booking/history/{emailId}");
        var response =
            await _httpService.Get<List<BookingOutput>>(url);

        return Ok(response.Result);
    }

    [HttpGet]
    [Route("booking/{pnr}")]
    public async Task<ActionResult> GetPNRBookings(string pnr)
    {
        var url = new Uri(new Uri(_baseUrl), $"booking/booking/{pnr}");
        var response =
            await _httpService.Get<List<BookingOutput>>(url);

        return Ok(response.Result);
    }

    [HttpDelete]
    [Route("cancel/{pnr}")]
    public async Task<ActionResult> CancelBookinng(string pnr)
    {
        var url = new Uri(new Uri(_baseUrl), $"booking/cancel/{pnr}");
        var response =
            await _httpService.Delete<object>(url);

        return Ok(response.Result);
    }
}