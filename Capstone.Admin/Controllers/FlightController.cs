using Capstone.Admin.Models;
using Capstone.Shared;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Admin.Controllers;

[ApiController]
[Route("admin/api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IHttpService _httpService;
    private readonly IConfiguration _configuration;
    private readonly string? _baseUrl;


    public FlightController(IHttpService httpService, IConfiguration configuration)
    {
        _httpService = httpService;
        _configuration = configuration;
        _baseUrl = configuration["FlightApiURL"];
    }

    [HttpGet]
    [Route("get-all-flights")]
    [Authorize]
    public async Task<ActionResult> GetAsync()
    {
        var url = new Uri(new Uri(_baseUrl), "flight");
        var response =
            await _httpService.Get<List<FlightOutput>>(url);

        return Ok(response.Result);
    }

    [HttpPost]
    [Route("add-flight")]
    [Authorize]
    public async Task<ActionResult> AddFlight(FlightInput flightInput)
    {
        var url = new Uri(new Uri(_baseUrl), "flight/add-flight");

        var response =
            await _httpService.Post<FlightInput, int>(url,
                flightInput);

        if (response.success)
        {
            return Ok(response.Result);
        }

        return StatusCode(response.ErrorCode);
    }

    [Route("block-unblock-flight/{flightId}/{blocked}")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> BlockUnblockFlight(int flightId, bool blocked)
    {
        var url1 = $"flight/block-unblock-flight/{flightId}/{blocked.ToString()}";
        var url = new Uri(new Uri(_baseUrl), url1);
        var response =
            await _httpService.Put<FlightInput, object>(url, null);

        if (response.success)
        {
            return Ok(response.Result);
        }

        return StatusCode(response.ErrorCode);
    }
}