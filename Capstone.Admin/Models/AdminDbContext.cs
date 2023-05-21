using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Admin.Models;

public class AdminDbContext : IdentityDbContext<Admin>
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
    {
    }


    public DbSet<Admin> Admins { get; set; }
}