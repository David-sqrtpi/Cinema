namespace Cinema.Integration.Commands;
public class VerifyAvailabilityCommand
{
    public Guid FunctionId { get; init; }
    public int Seats { get; init; }
}
