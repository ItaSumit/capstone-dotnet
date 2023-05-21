using Capstone.Flight.Models;
using Capstone.Flight.Services.Impl;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Flight.Test;

public class FlightServiceTest
{
    private FlightDbContext dbContext;

    [OneTimeSetUp]
    public void Setup()
    {
        dbContext = DbContextMocker.GetMockFlightContext();
    }

    [Test]
    public async Task should_return_all_flights()
    {
        var service = new FlightService(dbContext);

        var response = await service.GetAllFlights();

        Assert.That(response, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task should_return_flight_based_on_filter()
    {
        var service = new FlightService(dbContext);

        var response = await service.GetFlight(f => f.FlightNumber == "F-001");

        Assert.That(response, Is.Not.Null);
    }

    [Test]
    public async Task should_return_no_flight_based_on_filter()
    {
        var service = new FlightService(dbContext);

        var response = await service.GetFlight(f => f.FlightNumber == "F-003");

        Assert.That(response, Is.Null);
    }

    [Test]
    public async Task should_return_one_flight_for_one_way_search()
    {
        var service = new FlightService(dbContext);
        int daysToAddForMonday = (1 - (int)DateTime.Now.DayOfWeek + 7) % 7;
        var nextModay = DateTime.Now.AddDays(daysToAddForMonday);

        var flights = await service.SearchFlights(new FlightSearchCriteria
        {
            From = "BLR",
            To = "DEL",
            FromTravelDate = DateOnly.FromDateTime(nextModay),
            ReturnTravelDate = null,
            TripType = TripType.OneWay,
            MealType = MealType.Veg
        });

        Assert.That(flights, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task should_return_two_flight_for_one_way_search()
    {
        var service = new FlightService(dbContext);
        int daysToAddForMonday = (1 - (int)DateTime.Now.DayOfWeek + 7) % 7;
        var nextModay = DateTime.Now.AddDays(daysToAddForMonday);

        int daysToAddForTuesday = (2 - (int)DateTime.Now.DayOfWeek + 7) % 7;
        var nextTuesday = DateTime.Now.AddDays(daysToAddForTuesday);

        var flights = await service.SearchFlights(new FlightSearchCriteria
        {
            From = "BLR",
            To = "DEL",
            FromTravelDate = DateOnly.FromDateTime(nextModay),
            ReturnTravelDate = DateOnly.FromDateTime(nextTuesday),
            TripType = TripType.RoundTrip,
            MealType = MealType.Veg
        });

        Assert.That(flights, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task should_add_flight()
    {
        var newFlight = new FlightInput()
        {
            FlightNumber = "F-003",
            Airline = "Vistara",
            From = "BLR",
            To = "DEL",
            StartAt = TimeOnly.Parse("12:00"),
            EndAt = TimeOnly.Parse("14:00"),
            Days = new[] { "Mnday", "Tuesday" },
            Instrument = "Boeing",
            BusinessClassSeats = 10,
            NonBusinessClassSeats = 10,
            Rows = 5,
            Cost = 1000,
            IsVeg = true,
            IsNonVeg = true,
        };
        var service = new FlightService(dbContext);

        var id = await service.AddFlight(newFlight);

        var flights = await service.GetAllFlights();
        Assert.That(flights, Has.Count.EqualTo(3));

        dbContext.Flights.Remove(flights.Where(f => f.Id == id).First());
    }

    [Test]
    public async Task should_block_flight()
    {
        var service = new FlightService(dbContext);

        await service.BlockFlight(1, true);

        var flight = await service.GetFlight(f => f.Id == 1);
        Assert.That(flight.IsBlocked, Is.True);

        flight.IsBlocked = false;
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task should_un_block_flight()
    {
        var service = new FlightService(dbContext);

        await service.BlockFlight(1, false);

        var flight = await service.GetFlight(f => f.Id == 1);
        Assert.That(flight.IsBlocked, Is.False);
    }
}