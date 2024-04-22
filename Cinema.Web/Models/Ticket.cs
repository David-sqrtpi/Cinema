using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models;

public partial class Ticket
{
	[Display(Name = "Ticket")]
	public Guid TicketId { get; set; }

	[Display(Name = "Function")]
	public Guid FunctionId { get; set; }

	[Display(Name = "Additional Price")]
	public decimal? AdditionalPrice { get; set; }

	[Display(Name = "Name")]
	public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Seats { get; set; }
}
