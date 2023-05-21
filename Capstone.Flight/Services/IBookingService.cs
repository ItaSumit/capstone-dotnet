using Capstone.Flight.Models;
using Capstone.Shared.Domain;

namespace Capstone.Flight.Services;

public interface IBookingService
{
    Task<string> BookFlight(BookingInput bookingInput);

    Task CancelBooking(string pnr);

    IQueryable<Booking> GetBookings();
}