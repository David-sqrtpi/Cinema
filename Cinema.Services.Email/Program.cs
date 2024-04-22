using Cinema.Services.Email.Consumers;
using Cinema.Services.Email.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IMovieService, MovieService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Movies"]!); });
builder.Services.AddHttpClient<IFunctionService, FunctionService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Functions"]!); });
builder.Services.AddHttpClient<ITicketService, TicketService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Tickets"]!); });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TicketPurchasedConsumer>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["ConnectionStrings:ServiceBus"]);

        cfg.ConfigureEndpoints(context);
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
});

var app = builder.Build();

app.Run();
