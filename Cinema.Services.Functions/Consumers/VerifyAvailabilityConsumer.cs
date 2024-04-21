using Cinema.Integration.Commands;
using Cinema.Services.Functions.Models;
using MassTransit;

namespace Cinema.Services.Functions.Consumers;

public class VerifyAvailabilityConsumer(FunctionsContext functionsContext) : IConsumer<VerifyAvailabilityCommand>
{
    public async Task Consume(ConsumeContext<VerifyAvailabilityCommand> context)
    {
        var function = await functionsContext.Functions.FindAsync(context.Message.FunctionId);
        if (function is null)
            return;

        var seatsRequired = context.Message.Seats;
        var seatsAvailable = function.AvailableSeats;
        var isAvailableSeats = seatsRequired <= seatsAvailable;

        await context.RespondAsync<VerifyAvailabilityResponse>(new
        {
            context.Message.FunctionId,
            Seats = seatsRequired,
            IsAvailableSeats = isAvailableSeats
        });
    }
}
