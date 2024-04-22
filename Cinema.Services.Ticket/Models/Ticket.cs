using System;
using System.Collections.Generic;

namespace Cinema.Services.Ticket.Models;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public Guid FunctionId { get; set; }

    public decimal? AdditionalPrice { get; set; }

    public string UserName { get; set; } = null!;

    public int Seats { get; set; }

    public string Email { get; set; } = null!;
}
