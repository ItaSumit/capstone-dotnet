using System.Linq.Expressions;
using Capstone.Flight.Models;
using Capstone.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Flight.Services.Impl;

public class FlightService : IFlightService
{
    private readonly FlightDbContext _context;
    private readonly PricingStrategyCalculator _priceCalculator;

    public FlightService(FlightDbContext context, PricingStrategyCalculator priceCalculator)
    {
        _context = context;
        _priceCalculator = priceCalculator;
    }

    public async Task<List<Flight.Models.Flight>> GetAllFlights()
    {
        var flights = await _context.Flights.ToListAsync();
        return flights;
    }

    public async Task<Models.Flight> GetFlight(Expression<Func<Models.Flight, bool>> filter)
    {
        var flights = await _context.Flights.Where(filter).ToListAsync();
        return flights.FirstOrDefault();
    }

    public async Task<List<FlightResult>> SearchFlights(FlightSearchCriteria criteria)
    {
        var fromTravelDay = criteria.FromTravelDate.DayOfWeek.ToString();

        string returnTravelDay = "";
        if (criteria.TripType == TripType.RoundTrip)
        {
            returnTravelDay = criteria.ReturnTravelDate.GetValueOrDefault().DayOfWeek.ToString();
        }

        var flights = await _context.Flights.Where(f =>
            !f.IsBlocked &&
            (criteria.MealType == MealType.Veg && f.IsVeg || criteria.MealType == MealType.NonVeg && f.IsNonVeg)
            && (f.From == criteria.From && f.To == criteria.To && f.Days.Contains(fromTravelDay)
                || (criteria.TripType == TripType.RoundTrip && returnTravelDay != ""
                                                            && f.From == criteria.To && f.To == criteria.From &&
                                                            f.Days.Contains(returnTravelDay)))).ToListAsync();


        var flightResults = new List<FlightResult>();

        flights.ForEach(flight =>
        {
            var flightBookings = _context.Bookings.Where(b => b.Flight.FlightNumber == flight.FlightNumber)
                .Include(b => b.Passengers).ToList();
            var occupancy = flightBookings.Sum(b => b.Passengers.Count);

            var occupancyPercent = occupancy * 100 / (flight.BusinessClassSeats + flight.NonBusinessClassSeats);
            var flightResult = new FlightResult
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                Airline = flight.Airline,
                From = flight.From,
                To = flight.To,
                Instrument = flight.Instrument,
                MealType = criteria.MealType,
                Departure = flight.From == criteria.From
                    ? criteria.FromTravelDate.ToDateTime(flight.StartAt)
                    : criteria.ReturnTravelDate.GetValueOrDefault().ToDateTime(flight.StartAt),
                Arrival = flight.From == criteria.From
                    ? criteria.FromTravelDate.ToDateTime(flight.EndAt)
                    : criteria.ReturnTravelDate.GetValueOrDefault().ToDateTime(flight.EndAt),
                Cost = _priceCalculator.Calculate(occupancyPercent, flight.Cost)
            };
            flightResults.Add(flightResult);
        });
        return flightResults;
    }

    public async Task<int> AddFlight(FlightInput flightInput)
    {
        var flight = new Models.Flight
        {
            Airline = flightInput.Airline,
            FlightNumber = flightInput.FlightNumber,
            From = flightInput.From,
            To = flightInput.To,
            StartAt = flightInput.StartAt,
            EndAt = flightInput.EndAt,
            Instrument = flightInput.Instrument,
            IsNonVeg = flightInput.IsNonVeg,
            IsVeg = flightInput.IsVeg,
            Cost = flightInput.Cost,
            Days = string.Join(";", flightInput.Days),
            NonBusinessClassSeats = flightInput.NonBusinessClassSeats,
            BusinessClassSeats = flightInput.BusinessClassSeats,
            Rows = flightInput.Rows
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        return flight.Id;
    }

    public async Task BlockFlight(int flightId, bool isBlocked)
    {
        var flight = _context.Find<Models.Flight>(flightId);
        flight.IsBlocked = isBlocked;

        await _context.SaveChangesAsync();
    }
}