using Microsoft.EntityFrameworkCore;

namespace Capstone.Admin.Models;

public class AdminDbContext : DbContext
{
    public AdminDbContext()
    {
    }

    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
    {
    }
    
    public DbSet<Flight> Flights { get; set; }
}