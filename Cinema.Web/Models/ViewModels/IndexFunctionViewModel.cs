using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models.ViewModels;

public class IndexFunctionViewModel
{
	[Display(Name = "Function")]
	public Guid FunctionId { get; set; }

	public Movie Movie { get; set; } = null!;

    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal Price { get; set; }

	[Display(Name = "Function Date")]
	public DateTime FunctionDate { get; set; }

	[Display(Name = "Available Seats")]
	public int AvailableSeats { get; set; }
}
