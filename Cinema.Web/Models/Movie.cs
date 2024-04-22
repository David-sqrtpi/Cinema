using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models;

public partial class Movie
{
	[Display(Name = "Movie")]
	public Guid MovieId { get; set; }

    public string Title { get; set; } = null!;

    public int Year { get; set; }

    public string Genre { get; set; } = null!;

    public string Director { get; set; } = null!;

    public string Cast { get; set; } = null!;

    [Display(Name = "Poster Image")]
    public string? PosterImage { get; set; }
}
