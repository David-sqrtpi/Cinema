using Cinema.Integration.Events;
using Cinema.Services.Functions.Models;
using Cinema.Services.Functions.Services;
using MassTransit;

namespace Cinema.Services.Functions.Consumers;

public class TicketPurchasedConsumer(FunctionsContext db,
    ITicketService tickets) : IConsumer<TicketPurchasedEvent>
{
    public async Task Consume(ConsumeContext<TicketPurchasedEvent> context)
    {
        var ticket = await tickets.Get(context.Message.TicketId);

        Console.WriteLine("Reducing number of seats");
        Console.WriteLine($"Number of seats: {ticket.Seats}");


        var function = await db.Functions.FindAsync(ticket.FunctionId);

        function.AvailableSeats -= ticket.Seats;

        db.Functions.Update(function);
        await db.SaveChangesAsync();
    }
}
