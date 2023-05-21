using Capstone.Flight.Services;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Flight.Controllers;

[Route("flight/api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightRepository;

    public FlightController(IFlightService flightRepository)
    {
        _flightRepository = flightRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var flights = await _flightRepository.GetAllFlights();
        return Ok(flights);
    }

    [Route("search")]
    [HttpPost]
    public async Task<IActionResult> SearchFlight(FlightSearchCriteria criteria)
    {
        var flights = await _flightRepository.SearchFlights(criteria);
        return Ok(flights);
    }


    [Route("add-flight")]
    [HttpPost]
    public async Task<ActionResult> AddFlight(FlightInput flightInput)
    {
        var existing = await _flightRepository.GetFlight(x => x.FlightNumber == flightInput.FlightNumber);
        if (existing == null)
        {
            var result = await _flightRepository.AddFlight(flightInput);
            return Ok(result);
        }

        return Conflict(existing);
    }

    [Route("block-unblock-flight/{flightId}/{blocked}")]
    [HttpPut]
    public async Task<ActionResult> BlockUnblockFlight(int flightId, bool blocked)
    {
        await _flightRepository.BlockFlight(flightId, blocked);
        return Ok();
    }
}