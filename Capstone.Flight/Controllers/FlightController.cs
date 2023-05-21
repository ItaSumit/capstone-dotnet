using Capstone.Flight.Services;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Flight.Controllers;

[Route("flight/api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    public FlightController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromServices] IFlightService flightRepository)
    {
        var flights = await flightRepository.GetAllFlights();
        return Ok(flights);
    }

    [Route("search")]
    [HttpPost]
    public async Task<IActionResult> SearchFlight(FlightSearchCriteria criteria,
        [FromServices] IFlightService flightRepository)
    {
        var flights = await flightRepository.SearchFlights(criteria);
        return Ok(flights);
    }


    [Route("add-flight")]
    [HttpPost]
    public async Task<ActionResult> AddFlight(FlightInput flightInput, [FromServices] IFlightService flightRepository)
    {
        var existing = await flightRepository.GetFlight(x => x.FlightNumber == flightInput.FlightNumber);
        if (existing == null)
        {
            var result = await flightRepository.AddFlight(flightInput);
            return Ok(result);
        }

        return Conflict(existing);
    }

    [Route("block-unblock-flight/{flightId}/{blocked}")]
    [HttpPut]
    public async Task<ActionResult> BlockUnblockFlight(int flightId, bool blocked,
        [FromServices] IFlightService flightRepository)
    {
        await flightRepository.BlockFlight(flightId, blocked);
        return Ok();
    }
}