using System.Linq.Expressions;
using Capstone.Flight.Controllers;
using Capstone.Flight.Services;
using Capstone.Shared.Domain;
using Moq;

namespace Capstone.Flight.Test;

public class BookingControllerTest
{
    private readonly Mock<IBookingService> _mockBookingService;
    private readonly Mock<IFlightService> _mockFlightService;
    private readonly BookingController _bookingController;

    public BookingControllerTest()
    {
        _mockBookingService = new Mock<IBookingService>();
        _mockFlightService = new Mock<IFlightService>();
        _bookingController = new BookingController(_mockBookingService.Object, _mockFlightService.Object);
    }

    [Test]
    public void should_call_get_booking()
    {
        _bookingController.GetBookings();
        _mockBookingService.Verify(m => m.GetBookings(), Times.Once);
    }

    [Test]
    public void should_call_get_user_booking()
    {
        _bookingController.GetUserBooking("a@g.com");
        _mockBookingService.Verify(m => m.GetBookings(), Times.AtLeastOnce);
    }

    [Test]
    public async Task should_call_book_flight()
    {
        var bookingInput = new BookingInput
        {
            FromFlightId = 10,
            ReturnFlightId = 12,
            UserEmail = "a@a.cxom",
            Passengers = new List<PassengerInput>(),
            FromTravelDate = DateOnly.Parse("2023-05-21"),
            TripType = TripType.OneWay,
            ReturnTravelDate = null
        };

        var flight = new Models.Flight();

        _mockFlightService.Setup(m => m.GetFlight(It.IsAny<Expression<Func<Models.Flight, bool>>>()))
            .ReturnsAsync(flight);

        await _bookingController.BookFlight(bookingInput);
        _mockBookingService.Verify(m => m.BookFlight(bookingInput), Times.Once);
    }
}