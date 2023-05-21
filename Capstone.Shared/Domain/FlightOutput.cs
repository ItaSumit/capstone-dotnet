namespace Capstone.Shared.Domain;

public class FlightOutput
{
    public int Id { get; set; }

    public string FlightNumber { get; set; }

    public string Airline { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public TimeOnly StartAt { get; set; }

    public TimeOnly EndAt { get; set; }

    //public string[] Days { get; set; }

    public string Days { get; set; }

    public string Instrument { get; set; }

    public int BusinessClassSeats { get; set; }

    public int NonBusinessClassSeats { get; set; }

    public int Rows { get; set; }

    public double Cost { get; set; }

    public bool IsVeg { get; set; }

    public bool IsNonVeg { get; set; }

    public bool IsBlocked { get; set; }
}