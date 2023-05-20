using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Capstone.Admin.Migrations
{
    /// <inheritdoc />
    public partial class initiial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightNumber = table.Column<string>(type: "text", nullable: false),
                    Airline = table.Column<string>(type: "text", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    StartAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndAt = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Days = table.Column<string>(type: "text", nullable: false),
                    Instrument = table.Column<string>(type: "text", nullable: false),
                    BusinessClassSeats = table.Column<int>(type: "integer", nullable: false),
                    NonBusinessClassSeats = table.Column<int>(type: "integer", nullable: false),
                    Rows = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<double>(type: "double precision", nullable: false),
                    IsVeg = table.Column<bool>(type: "boolean", nullable: false),
                    IsNonVeg = table.Column<bool>(type: "boolean", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
