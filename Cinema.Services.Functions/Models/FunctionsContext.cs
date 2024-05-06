using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Functions.Models;

public partial class FunctionsContext(IConfiguration configuration) : DbContext
{
    public virtual DbSet<Function> Functions { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	=> optionsBuilder.UseSqlServer(configuration["ConnectionStrings:FunctionsDb"]);
}
