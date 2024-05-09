using Cinema.Services.Ticket.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Cinema.Integration.Commands;
using Cinema.Integration.Events;
using Microsoft.OpenApi.Models;
using Cinema.Services.Ticket.Enums;
using Cinema.Services.Ticket.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddServer(new OpenApiServer { Url = "/" });
    x.AddServer(new OpenApiServer { Url = "https://final-tickets.azurewebsites.net" });
});

builder.Services.AddDbContext<TicketsContext>();
builder.Services.AddDbContext<LiteTicketsContext>();

var databaseProvider = builder.Configuration["DatabaseProvider"];

_ = databaseProvider switch
{
	nameof(DatabaseProviders.SqlServer)
		=> builder.Services.AddTransient<ITicketRepository, TicketRepository>(),

	_ => builder.Services.AddTransient<ITicketRepository, LiteTicketRepository>()
};

builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["ConnectionStrings:ServiceBus"]);

        cfg.ConfigureEndpoints(context);
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<ITicketRepository>();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async () =>
{
    return await db.ReadAll();
});

app.MapGet("/details/{ticketId}", async Task<Results<Ok<Ticket>, NotFound>> (Guid ticketId) =>
{
    var ticket = await db.ReadById(ticketId);

    if (ticket == null) return TypedResults.NotFound();

    return TypedResults.Ok(ticket);
});

app.MapPost("/create", async Task<Results<Ok, BadRequest>> ([FromServices] IRequestClient<VerifyAvailabilityCommand> requestClient, [FromServices] IBus bus, Ticket ticket) => 
{
    var verifyAvailability = new VerifyAvailabilityCommand
    {
        FunctionId = ticket.FunctionId,
        Seats = ticket.Seats
    };

    var response = await requestClient.GetResponse<VerifyAvailabilityResponse>(verifyAvailability);

    if (!response.Message.IsAvailableSeats)
        return TypedResults.BadRequest();

    await db.Create(ticket);

    var ticketPurchasedEvent = new TicketPurchasedEvent
    {
        TicketId = ticket.TicketId
    };

    await bus.Publish(ticketPurchasedEvent);

    return TypedResults.Ok();
});

app.MapPost("/edit", async (Ticket ticket) =>
{
    await db.Update(ticket);
});

app.MapPost("/delete", async (Guid ticketId) =>
{
    await db.DeleteById(ticketId);
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