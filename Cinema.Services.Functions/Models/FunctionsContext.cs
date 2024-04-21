using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Functions.Models;

public partial class FunctionsContext(
    DbContextOptions<FunctionsContext> options, 
    IConfiguration configuration) : DbContext(options)
{
    public virtual DbSet<Function> Functions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(configuration["db:functions"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.FunctionId).HasName("PK__Function__43D3C33AA0D85188");

            entity.ToTable("Function");

            entity.Property(e => e.FunctionId);
            entity.Property(e => e.FunctionDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
