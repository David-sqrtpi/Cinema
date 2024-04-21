using Cinema.Services.Ticket.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Cinema.Integration.Commands;
using Cinema.Integration.Events;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddServer(new OpenApiServer { Url = "/" });
    x.AddServer(new OpenApiServer { Url = "https://final-tickets.azurewebsites.net" });
});

builder.Services.AddDbContext<TicketsContext>();

builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["serviceBus:connectionString"]);

        cfg.ConfigureEndpoints(context);
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
});

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", (TicketsContext db) =>
{
    return db.Tickets.ToListAsync();
});

app.MapGet("/details/{ticketId}", async Task<Results<Ok<Ticket>, NotFound>> (TicketsContext db, Guid ticketId) =>
{
    var ticket = await db.Tickets.Where(x => x.TicketId == ticketId).FirstOrDefaultAsync();

    if (ticket == null) return TypedResults.NotFound();

    return TypedResults.Ok(ticket);
});

app.MapPost("/create", async Task<Results<Ok, BadRequest>> (TicketsContext db, [FromServices] IRequestClient<VerifyAvailabilityCommand> requestClient, [FromServices] IBus bus, Ticket ticket) => 
{
    var verifyAvailability = new VerifyAvailabilityCommand
    {
        FunctionId = ticket.FunctionId,
        Seats = ticket.Seats
    };

    var response = await requestClient.GetResponse<VerifyAvailabilityResponse>(verifyAvailability);

    if (!response.Message.IsAvailableSeats)
        return TypedResults.BadRequest();

    db.Tickets.Add(ticket);
    await db.SaveChangesAsync();

    var ticketPurchasedEvent = new TicketPurchasedEvent
    {
        TicketId = ticket.TicketId
    };

    await bus.Publish(ticketPurchasedEvent);

    return TypedResults.Ok();
});

app.MapPost("/edit", async (TicketsContext db, Ticket ticket) =>
{
    db.Tickets.Update(ticket);
    await db.SaveChangesAsync();
});

app.MapPost("/delete", async (TicketsContext db, Guid ticketId) =>
{
    var ticket = await db.Tickets.FindAsync(ticketId);

    if (ticket is not null)
        db.Tickets.Remove(ticket);

    await db.SaveChangesAsync();
});

app.Run();

//public class Test(IBus _bus) : BackgroundService
//{
//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            await _bus.Publish(new VerifyAvailabilityCommand { });

//            await Task.Delay(1000, stoppingToken);
//        }
//    }
//}