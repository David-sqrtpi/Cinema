using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Ticket.Models;

public partial class TicketsContext(IConfiguration configuration) : DbContext
{
    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(configuration["ConnectionStrings:TicketsDb"]);
}
