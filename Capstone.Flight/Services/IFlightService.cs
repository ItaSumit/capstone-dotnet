using System.Linq.Expressions;
using Capstone.Shared.Domain;

namespace Capstone.Flight.Services;

public interface IFlightService
{
    Task<List<Models.Flight>> GetAllFlights();
    Task<Models.Flight> GetFlight(Expression<Func<Models.Flight, bool>> filter);
    Task<List<FlightResult>> SearchFlights(FlightSearchCriteria criteria);
    Task AddFlight(FlightInput flightInput);
    Task BlockFlight(int flightId, bool isBlocked);
}