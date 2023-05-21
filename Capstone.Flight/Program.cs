using Capstone.Flight.Models;
using Capstone.Flight.Services;
using Capstone.Flight.Services.Impl;
using Common.Shared.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    o.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlightDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("capstone_db"),
        option => { option.MigrationsHistoryTable("__EF_MigrationsHistory_flight"); }));

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<PricingStrategyCalculator>();
builder.Services.AddScoped<HikedPricingStrategy>();
builder.Services.AddScoped<NormalPricingStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RunMigration(app);

app.Run();

void RunMigration(IHost app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider
        .GetRequiredService<FlightDbContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}