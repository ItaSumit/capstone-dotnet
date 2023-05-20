using Microsoft.EntityFrameworkCore;

namespace Capstone.Flight.Models;

public class FlightDbContext : DbContext
{
    public FlightDbContext()
    {
    }

    public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
    {
    }
    
    public DbSet<Flight> Flights { get; set; }
}