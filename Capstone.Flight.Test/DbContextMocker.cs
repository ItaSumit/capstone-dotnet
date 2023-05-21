using Capstone.Flight.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Flight.Test;

public static class DbContextMocker
{
    //https://www.codingame.com/playgrounds/35462/creating-web-api-in-asp-net-core-2-0/part-2---unit-tests
    public static FlightDbContext GetMockFlightContext()
    {
        // Create options for DbContext instance
        var options = new DbContextOptionsBuilder<FlightDbContext>()
            .UseInMemoryDatabase(databaseName: "flightDb")
            .Options;

        // Create instance of DbContext
        var dbContext = new FlightDbContext(options);

        // Add entities in memory
        dbContext.Seed();

        return dbContext;
    }
}