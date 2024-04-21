using System;
using System.Collections.Generic;

namespace Cinema.Services.Movies.Models;

public partial class Movie
{
    public Guid MovieId { get; set; }

    public string Title { get; set; } = null!;

    public int Year { get; set; }

    public string Genre { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Cast { get; set; } = null!;

    public string? PosterImage { get; set; }
}
