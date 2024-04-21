using System;
using System.Collections.Generic;

namespace Cinema.Services.Functions.Models;

public partial class Function
{
    public Guid FunctionId { get; set; }

    public Guid MovieId { get; set; }

    public decimal Price { get; set; }

    public DateTime FunctionDate { get; set; }

    public int AvailableSeats { get; set; }
}
