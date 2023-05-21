using System.Text.Json.Serialization;
using Capstone.Shared.Domain;

namespace Capstone.Flight.Models;

public class Booking
{
    public int Id { get; set; }

    public string PNR { get; set; }

    public int FlightId { get; set; }

    public virtual Flight Flight { get; set; }

    public string EmailId { get; set; }

    public DateOnly StartDate { get; set; }

    public TripType TripType { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public ICollection<Passenger> Passengers { get; set; }

    public Direction Direction { get; set; }

    public Status Status { get; set; }
}