using System.Linq.Expressions;
using Capstone.Flight.Controllers;
using Capstone.Flight.Services;
using Capstone.Shared.Domain;
using Moq;

namespace Capstone.Flight.Test;

public class FlightControllerTest
{
    private readonly Mock<IFlightService> _mockFlightService;
    private readonly FlightController _flightController;

    public FlightControllerTest()
    {
        _mockFlightService = new Mock<IFlightService>();
        _flightController = new FlightController(_mockFlightService.Object);
    }

    [Test]
    public async Task should_call_get_all_flights()
    {
        await _flightController.GetAsync();
        _mockFlightService.Verify(m => m.GetAllFlights(), Times.Once);
    }

    [Test]
    public async Task should_call_SearchFlight()
    {
        var criteria = new FlightSearchCriteria
        {
            From = "DEL",
            To = "BLR",
            FromTravelDate = DateOnly.Parse("2023-05-21"),
            ReturnTravelDate = null,
            TripType = TripType.OneWay,
            MealType = MealType.Veg
        };
        await _flightController.SearchFlight(criteria);
        _mockFlightService.Verify(m => m.SearchFlights(criteria), Times.Once);
    }

    [Test]
    public async Task should_call_Add_FLight()
    {
        var newFlight = new FlightInput
        {
            FlightNumber = "F-001",
            Airline = "Indigo",
            From = "BLR",
            To = "DEL",
            StartAt = TimeOnly.Parse("12:00"),
            EndAt = TimeOnly.Parse("13:00"),
            Days = new string[]
            {
                "Monday"
            },
            Instrument = "Boeing",
            BusinessClassSeats = 10,
            NonBusinessClassSeats = 10,
            Rows = 5,
            Cost = 1000,
            IsVeg = true,
            IsNonVeg = false
        };
        await _flightController.AddFlight(newFlight);
        _mockFlightService.Verify(m => m.AddFlight(newFlight), Times.Once);
    }

    [Test]
    public async Task should_not_call_Add_FLight()
    {
        var flight = new FlightInput
        {
            FlightNumber = "F-001",
            Airline = "Indigo",
            From = "BLR",
            To = "DEL",
            StartAt = TimeOnly.Parse("12:00"),
            EndAt = TimeOnly.Parse("13:00"),
            Days = new string[]
            {
                "Monday"
            },
            Instrument = "Boeing",
            BusinessClassSeats = 10,
            NonBusinessClassSeats = 10,
            Rows = 5,
            Cost = 1000,
            IsVeg = true,
            IsNonVeg = false
        };
        
        var existingFlight = new Models.Flight();
        
        _mockFlightService.Setup(m =>
            m.GetFlight(It.IsAny<Expression<Func<Models.Flight, bool>>>())).ReturnsAsync(existingFlight);
        await _flightController.AddFlight(flight);
        _mockFlightService.Verify(m => m.AddFlight(flight), Times.Never);
    }

    [Test]
    public async Task should_call_block_fllight()
    {
        await _flightController.BlockUnblockFlight(10, true);
        _mockFlightService.Verify(m => m.BlockFlight(10, true), Times.Once);
    }
}