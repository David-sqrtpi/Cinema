using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Services.Functions.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailableSeatsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "Function",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "Function");
        }
    }
}
