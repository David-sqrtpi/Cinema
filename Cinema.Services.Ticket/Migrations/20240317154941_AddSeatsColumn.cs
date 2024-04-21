using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Services.Ticket.Migrations
{
    /// <inheritdoc />
    public partial class AddSeatsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Ticket");
        }
    }
}
