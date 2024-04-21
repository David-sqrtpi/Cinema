namespace Cinema.Integration.Events;

public record TicketPurchasedEvent
{
    public Guid TicketId { get; init; }
}
