using Capstone.Flight.Models;

namespace Capstone.Flight.Test;

public static class DbContextExtensions
{
    public static void Seed(this FlightDbContext dbContext)
    {
        dbContext.Flights.Add(new Models.Flight
        {
            Id = 1,
            FlightNumber = "F-001",
            Airline = "Vistara",
            From = "BLR",
            To = "DEL",
            StartAt = TimeOnly.Parse("12:00"),
            EndAt = TimeOnly.Parse("14:00"),
            Days = "Monday;Tuesday",
            Instrument = "Boeing",
            BusinessClassSeats = 10,
            NonBusinessClassSeats = 10,
            Rows = 5,
            Cost = 1000,
            IsVeg = true,
            IsNonVeg = true,
            IsBlocked = false
        });
        
        dbContext.Flights.Add(new Models.Flight
        {
            Id = 2,
            FlightNumber = "F-002",
            Airline = "Vistara",
            From = "DEL",
            To = "BLR",
            StartAt = TimeOnly.Parse("15:00"),
            EndAt = TimeOnly.Parse("17:00"),
            Days = "Monday;Tuesday",
            Instrument = "Boeing",
            BusinessClassSeats = 10,
            NonBusinessClassSeats = 10,
            Rows = 5,
            Cost = 1000,
            IsVeg = true,
            IsNonVeg = true,
            IsBlocked = false
        });
        dbContext.SaveChanges();
    }
}