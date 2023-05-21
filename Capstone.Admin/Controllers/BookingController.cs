using Capstone.Shared;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Admin.Controllers;

[ApiController]
[Route("admin/api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IHttpService _httpService;
    private readonly IConfiguration _configuration;
    private readonly string? _baseUrl;

    public BookingController(IHttpService httpService, IConfiguration configuration)
    {
        _httpService = httpService;
        _configuration = configuration;
        _baseUrl = configuration["FlightApiURL"];
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetBookings()
    {
        var url = new Uri(new Uri(_baseUrl), "booking");
        var response =
            await _httpService.Get<List<BookingOutput>>(url);

        return Ok(response.Result);
    }
}