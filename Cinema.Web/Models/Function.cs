using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models;

public partial class Function
{
	[Display(Name = "Function")]
	public Guid FunctionId { get; set; }

	[Display(Name = "Movie")]
	public Guid MovieId { get; set; }

    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal Price { get; set; }

    [Display(Name = "Function Date")]
    public DateTime FunctionDate { get; set; }

    [Range(1, int.MaxValue)]
	[Display(Name = "Available Seats")]
	public int AvailableSeats { get; set; }
}
