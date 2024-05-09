using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Movies.Models;

public partial class MoviesContext(IConfiguration config) : DbContext
{
    public virtual DbSet<Movie> Movies { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer(config["ConnectionStrings:MoviesDb"]);
}
