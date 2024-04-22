using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services.Movies.Models;

public partial class MoviesContext(
    DbContextOptions<MoviesContext> options,
    IConfiguration configuration) : DbContext(options)
{
    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(configuration["ConnectionStrings:MoviesDb"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__4BD2941A21FD7798");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
