using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Integration.Commands;

public class VerifyAvailabilityResponse
{
    public Guid? FunctionId { get; init; }
    public int Seats { get; init; }
    public bool IsAvailableSeats { get; init; }
}
