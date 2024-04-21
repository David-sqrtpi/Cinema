using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Ticket.Models;

public partial class TicketsContext(DbContextOptions<TicketsContext> options, IConfiguration configuration) : DbContext(options)
{
    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(configuration["ConnectionStrings:Tickets"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC6079113F60E");

            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId);
            entity.Property(e => e.AdditionalPrice).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
