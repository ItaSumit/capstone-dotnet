using Capstone.Flight.Models;
using Capstone.Shared.Domain;

namespace Capstone.Flight.Services.Impl;

public class BookingService : IBookingService
{
    private readonly FlightDbContext _context;

    public BookingService(FlightDbContext context)
    {
        _context = context;
    }

    public IQueryable<Booking> GetBookings()
    {
        return _context.Bookings;
    }

    public async Task<string> BookFlight(BookingInput bookingInput)
    {
        var fromFlight = await _context.Flights.FindAsync(bookingInput.FromFlightId);
        // var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailId == bookingInput.UserEmail);
        var tripTypeCode = bookingInput.TripType == TripType.OneWay ? "OW" : "RT";
        var pnr =
            $"{fromFlight.From}-{fromFlight.To}-{tripTypeCode}-{Guid.NewGuid().ToString().Split("-")[0].ToUpper()}";

        var fromBooking = new Booking
        {
            FlightId = bookingInput.FromFlightId,
            EmailId = bookingInput.UserEmail,
            StartDate = bookingInput.FromTravelDate,
            TripType = bookingInput.TripType,
            ReturnDate = bookingInput.ReturnTravelDate,
            Passengers =
                bookingInput.Passengers.Select(p => new Passenger
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    MealType = p.MealType,
                    SeatNumber = p.SeatNumber,
                }).ToList(),
            PNR = pnr,
            Direction = Direction.Up,
            Status = Status.Confirmed
        };

        _context.Bookings.Add(fromBooking);


        if (bookingInput.TripType == TripType.RoundTrip && bookingInput.ReturnTravelDate != null &&
            bookingInput.ReturnFlightId != null)
        {
            var returnFlight = await _context.Flights.FindAsync(bookingInput.ReturnFlightId);


            var returnBooking = new Booking
            {
                FlightId = bookingInput.ReturnFlightId.GetValueOrDefault(),
                EmailId = bookingInput.UserEmail,
                StartDate = bookingInput.FromTravelDate,
                TripType = bookingInput.TripType,
                ReturnDate = bookingInput.ReturnTravelDate,
                Passengers = bookingInput.Passengers.Select(p => new Passenger
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    MealType = p.MealType,
                    SeatNumber = p.SeatNumber,
                }).ToList(),
                PNR = pnr,
                Direction = Direction.Down,
                Status = Status.Confirmed
            };

            _context.Bookings.Add(returnBooking);
        }

        await _context.SaveChangesAsync();
        return pnr;
    }

    public async Task CancelBooking(string pnr)
    {
        var bookings = _context.Bookings.Where(b => b.PNR == pnr).ToList();

        foreach (var booking in bookings)
        {
            booking.Status = Status.Cancelled;
        }

        await _context.SaveChangesAsync();
    }
}