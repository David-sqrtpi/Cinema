using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Movies.Models;

public class LiteMoviesContext(IConfiguration config) : MoviesContext(config)
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlite("Data Source=Movies.db");
}