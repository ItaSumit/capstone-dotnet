﻿// <auto-generated />
using System;
using Capstone.Flight.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Capstone.Flight.Migrations
{
    [DbContext(typeof(FlightDbContext))]
    partial class FlightDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Capstone.Flight.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Direction")
                        .HasColumnType("integer");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FlightId")
                        .HasColumnType("integer");

                    b.Property<string>("PNR")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("ReturnDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TripType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Capstone.Flight.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BusinessClassSeats")
                        .HasColumnType("integer");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<string>("Days")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeOnly>("EndAt")
                        .HasColumnType("time without time zone");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Instrument")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsNonVeg")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVeg")
                        .HasColumnType("boolean");

                    b.Property<int>("NonBusinessClassSeats")
                        .HasColumnType("integer");

                    b.Property<int>("Rows")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("StartAt")
                        .HasColumnType("time without time zone");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Capstone.Flight.Models.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int>("BookingId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MealType")
                        .HasColumnType("integer");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Passenger");
                });

            modelBuilder.Entity("Capstone.Flight.Models.Booking", b =>
                {
                    b.HasOne("Capstone.Flight.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Capstone.Flight.Models.Passenger", b =>
                {
                    b.HasOne("Capstone.Flight.Models.Booking", "Booking")
                        .WithMany("Passengers")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("Capstone.Flight.Models.Booking", b =>
                {
                    b.Navigation("Passengers");
                });
#pragma warning restore 612, 618
        }
    }
}
