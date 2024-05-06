using Cinema.Integration.Events;
using Cinema.Services.Functions.Models;
using Cinema.Services.Functions.Repositories;
using Cinema.Services.Functions.Services;
using MassTransit;

namespace Cinema.Services.Functions.Consumers;

public class TicketPurchasedConsumer(IFunctionsRepository functionsDb,
    ITicketService tickets) : IConsumer<TicketPurchasedEvent>
{
    public async Task Consume(ConsumeContext<TicketPurchasedEvent> context)
    {
        var ticket = await tickets.Get(context.Message.TicketId);

        Console.WriteLine("Reducing number of seats");
        Console.WriteLine($"Number of seats: {ticket.Seats}");

        //var function = await functionsDb.Functions.FindAsync(ticket.FunctionId);
        var function = await functionsDb.ReadById(ticket.FunctionId);

        function.AvailableSeats -= ticket.Seats;

        //functionsDb.Functions.Update(function);
        //await functionsDb.SaveChangesAsync();

        await functionsDb.Update(function);
    }
}
