using Capstone.Flight.Models;
using Capstone.Flight.Services;
using Capstone.Flight.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlightDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("capstone_db"), option =>
    {
        option.MigrationsHistoryTable("__EF_MigrationsHistory_flight");
    }));

builder.Services.AddScoped<IFlightService, FlightService>();

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

app.Run();