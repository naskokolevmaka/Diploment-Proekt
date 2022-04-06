using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Migrations
{
    public partial class models2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleflightId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    destinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    distance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.destinationId);
                });

            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    airlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airlineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    destinationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.airlineId);
                    table.ForeignKey(
                        name: "FK_Airline_Destination_destinationId",
                        column: x => x.destinationId,
                        principalTable: "Destination",
                        principalColumn: "destinationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    flightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airlineId = table.Column<int>(type: "int", nullable: false),
                    destinationId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    scheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    departureTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.flightId);
                    table.ForeignKey(
                        name: "FK_Schedule_Airline_airlineId",
                        column: x => x.airlineId,
                        principalTable: "Airline",
                        principalColumn: "airlineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedule_Destination_destinationId",
                        column: x => x.destinationId,
                        principalTable: "Destination",
                        principalColumn: "destinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ticketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    flightId = table.Column<int>(type: "int", nullable: false),
                    ticketCount = table.Column<int>(type: "int", nullable: false),
                    dateOfJourney = table.Column<DateTime>(type: "datetime2", nullable: false),
                    airlineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Airline_airlineId",
                        column: x => x.airlineId,
                        principalTable: "Airline",
                        principalColumn: "airlineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Schedule_flightId",
                        column: x => x.flightId,
                        principalTable: "Schedule",
                        principalColumn: "flightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ScheduleflightId",
                table: "AspNetUsers",
                column: "ScheduleflightId");

            migrationBuilder.CreateIndex(
                name: "IX_Airline_destinationId",
                table: "Airline",
                column: "destinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_airlineId",
                table: "Schedule",
                column: "airlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_destinationId",
                table: "Schedule",
                column: "destinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_airlineId",
                table: "Ticket",
                column: "airlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_flightId",
                table: "Ticket",
                column: "flightId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Schedule_ScheduleflightId",
                table: "AspNetUsers",
                column: "ScheduleflightId",
                principalTable: "Schedule",
                principalColumn: "flightId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Schedule_ScheduleflightId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ScheduleflightId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ScheduleflightId",
                table: "AspNetUsers");
        }
    }
}
