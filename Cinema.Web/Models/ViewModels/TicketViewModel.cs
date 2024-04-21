using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models.ViewModels;

public class TicketViewModel
{
    public Function? Function { get; set; }

	[Display(Name = "Function")]
	public Guid FunctionId { get; set; }

    public Movie? Movie { get; set; }

    [Range(1, int.MaxValue)]
    public int Seats { get; set; }

    [Required]
	[Display(Name = "Name")]
	public string UserName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;
}
