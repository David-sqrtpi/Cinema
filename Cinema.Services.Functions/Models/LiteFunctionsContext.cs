using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Functions.Models;

public class LiteFunctionsContext(IConfiguration configuration) : FunctionsContext(configuration)
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlite("Data Source=Functions.db");
} 
