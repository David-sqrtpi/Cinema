using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Ticket.Models;

public partial class LiteTicketsContext(IConfiguration configuration) : TicketsContext(configuration)
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlite("Data Source=Tickets.db");
}