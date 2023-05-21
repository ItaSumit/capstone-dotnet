using Capstone.Shared;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.EndUser.Controllers;

[ApiController]
[Route("user/api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IHttpService _httpService;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl = "http://localhost:5001/flight/api/flight/";

    public FlightController(IHttpService httpService, IConfiguration configuration)
    {
        _httpService = httpService;
        _configuration = configuration;
        _baseUrl = configuration["FlightApiURL"];
    }

    [HttpPost]
    [Route("search-flights")]
    public async Task<ActionResult> Search(FlightSearchCriteria criteria)
    {
        var url = new Uri(new Uri(_baseUrl), "flight/search");
        var response =
            await _httpService.Post<FlightSearchCriteria, List<FlightResult>>(url, criteria);

        return Ok(response.Result);
    }
}