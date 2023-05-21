namespace Capstone.Shared.Domain;

public class BookingOutput
{
    public int BookingId { get; set; }

    public string Airline { get; set; }
    public string FlightNumer { get; set; }

    public string PNR { get; set; }
    public List<PassengerOutput> Passengers { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public DateOnly DepartureDate { get; set; }

    public TimeOnly DepartureTime { get; set; }
    public DateOnly ArraivalDate { get; set; }

    public TimeOnly ArrivalTime { get; set; }

    public string EmailId { get; set; }

    public Direction Direction { get; set; }


    public Status Status { get; set; }
}

public class PassengerOutput
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Age { get; set; }


    public MealType MealType { get; set; }


    public int SeatNumber { get; set; }
}