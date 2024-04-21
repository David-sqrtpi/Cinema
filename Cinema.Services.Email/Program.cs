using Cinema.Services.Email.Consumers;
using Cinema.Services.Email.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ITicketService, TicketService>();
builder.Services.AddHttpClient<IFunctionService, FunctionService>();
builder.Services.AddHttpClient<IMovieService, MovieService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TicketPurchasedConsumer>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["serviceBus:connectionString"]);

        cfg.ConfigureEndpoints(context);
    });

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
});

var app = builder.Build();

app.Run();
