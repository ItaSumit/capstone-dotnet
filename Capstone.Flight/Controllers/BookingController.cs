using Capstone.Flight.Services;
using Capstone.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Flight.Controllers;

[Route("flight/api/[controller]")]
[ApiController]
[Produces("application/json")]
public class BookingController : ControllerBase
{
    [HttpGet]
    public ActionResult GetBookings([FromServices] IBookingService bookingService)
    {
        var bookings = bookingService.GetBookings();
        List<BookingOutput> bookingsOutput = bookings.Select(b =>
            new BookingOutput
            {
                BookingId = b.Id,
                PNR = b.PNR,
                ArraivalDate = b.Direction == Direction.Up ? b.StartDate : b.ReturnDate.GetValueOrDefault(),
                ArrivalTime = b.Flight.EndAt,
                DepartureDate = b.Direction == Direction.Up ? b.StartDate : b.ReturnDate.GetValueOrDefault(),
                DepartureTime = b.Flight.StartAt,
                EmailId = b.EmailId,
                FlightNumer = b.Flight.FlightNumber,
                Airline = b.Flight.Airline,
                From = b.Flight.From,
                To = b.Flight.To,
                Direction = b.Direction,
                Status = b.Status,
                Passengers = b.Passengers.Select(p => new PassengerOutput()
                {
                    Age = p.Age,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    MealType = p.MealType,
                    SeatNumber = p.SeatNumber,
                }).ToList()
            }).ToList();


        return Ok(bookingsOutput);
    }

    [HttpGet]
    [Route("history/{emailId}")]
    public ActionResult GetUserBooking(string emailId, [FromServices] IBookingService bookingService)
    {
        var bookings = bookingService.GetBookings().Where(b => b.EmailId == emailId);
        List<BookingOutput> bookingsOutput = bookings.Select(b =>
            new BookingOutput
            {
                BookingId = b.Id,
                PNR = b.PNR,
                ArraivalDate = b.Direction == Direction.Up ? b.StartDate : b.ReturnDate.GetValueOrDefault(),
                ArrivalTime = b.Flight.EndAt,
                DepartureDate = b.Direction == Direction.Up ? b.StartDate : b.ReturnDate.GetValueOrDefault(),
                DepartureTime = b.Flight.StartAt,
                EmailId = b.EmailId,
                FlightNumer = b.Flight.FlightNumber,
                Airline = b.Flight.Airline,
                From = b.Flight.From,
                To = b.Flight.To,
                Direction = b.Direction,
                Status = b.Status,
                Passengers = b.Passengers.Select(p => new PassengerOutput()
                {
                    Age = p.Age,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    MealType = p.MealType,
                    SeatNumber = p.SeatNumber,
                }).ToList()
            }).ToList();
        return Ok(bookingsOutput);
    }

    [HttpGet]
    [Route("booking/{pnr}")]
    public async Task<IActionResult> GetPNRBookings(string pnr, [FromServices] IBookingService bookingService)
    {
        var bookings = bookingService.GetBookings().Where(b => b.PNR == pnr);
        List<BookingOutput> bookingsOutput = bookings.Select(b =>
            new BookingOutput
            {
                BookingId = b.Id,
                PNR = b.PNR,
                ArraivalDate = b.Direction == Direction.Up ? b.StartDate : b.ReturnDate.GetValueOrDefault(),
                ArrivalTime = b.Flight.EndAt,
                DepartureDate = b.Direction == Direction.Up ? b.StartDate : b.ReturnDate.GetValueOrDefault(),
                DepartureTime = b.Flight.StartAt,
                EmailId = b.EmailId,
                FlightNumer = b.Flight.FlightNumber,
                Airline = b.Flight.Airline,
                From = b.Flight.From,
                To = b.Flight.To,
                Direction = b.Direction,
                Status = b.Status,
                Passengers = b.Passengers.Select(p => new PassengerOutput()
                {
                    Age = p.Age,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    MealType = p.MealType,
                    SeatNumber = p.SeatNumber,
                }).ToList()
            }).ToList();
        return Ok(bookingsOutput);
    }

    [HttpPost]
    [Route("book-flight")]
    public async Task<ActionResult> BookFlight(BookingInput bookingInput,
        [FromServices] IFlightService flightService, [FromServices] IBookingService bookingService)
    {
        var errors = new List<string>();
        var fromFlight = await flightService.GetFlight(x => x.Id == bookingInput.FromFlightId);
        if (fromFlight == null || fromFlight.IsBlocked)
            errors.Add("From flight not avaialble or flight is blocked.");

        if (bookingInput.TripType == TripType.RoundTrip)
        {
            var returnFlight = await flightService.GetFlight(x => x.Id == bookingInput.ReturnFlightId);

            if (returnFlight == null || returnFlight.IsBlocked)
            {
                errors.Add("Return flight is not available or flight is blocked.");
            }
        }

        if (errors.Count > 0)
        {
            return BadRequest(errors);
        }

        var pnr = await bookingService.BookFlight(bookingInput);
        return Ok(pnr);
    }

    [HttpDelete]
    [Route("cancel/{pnr}")]
    public async Task<ActionResult> CancelBookinng(string pnr, [FromServices] IBookingService bookingService)
    {
        await bookingService.CancelBooking(pnr);
        return Ok("Booking cancelled successfully");
    }
}