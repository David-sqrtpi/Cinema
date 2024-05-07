using Cinema.Services.Functions.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MassTransit;
using Cinema.Services.Functions.Consumers;
using Cinema.Services.Functions.Services;
using Microsoft.OpenApi.Models;
using Cinema.Services.Functions.Enums;
using Cinema.Services.Functions.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddServer(new OpenApiServer { Url = "/" });
    x.AddServer(new OpenApiServer { Url = "https://final-functions.azurewebsites.net" });
});

var settings = builder.Configuration;

builder.Services.AddDbContext<FunctionsContext>();
builder.Services.AddDbContext<LiteFunctionsContext>();
builder.Services.AddHttpClient<IMovieService, MovieService>(c => { c.BaseAddress = new Uri(settings["Urls:Movies"]!); });
builder.Services.AddHttpClient<ITicketService, TicketService>(c => { c.BaseAddress = new Uri(settings["Urls:Tickets"]!); });

var databaseProvider = settings["DatabaseProvider"];

_ = databaseProvider switch
{
	nameof(DatabaseProviders.SqlServer)
		=> builder.Services.AddTransient<IFunctionsRepository, FunctionsRepository>(),

	_ => builder.Services.AddTransient<IFunctionsRepository, LiteFunctionsRepository>()
};

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<VerifyAvailabilityConsumer>();
	x.AddConsumer<TicketPurchasedConsumer>();

	x.UsingAzureServiceBus((context, cfg) =>
	{
		cfg.Host(settings["ConnectionStrings:ServiceBus"]);

        cfg.ConfigureEndpoints(context);
	});

	x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(true));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var functionsRepository = scope.ServiceProvider.GetRequiredService<IFunctionsRepository>();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (Guid? movieId) =>
{
	return await functionsRepository.ReadAll();
});

app.MapGet("/details/{id}", async Task<Results<Ok<Function>, NotFound>> (Guid id) =>
{
	var function = await functionsRepository.ReadById(id);
	if (function is null) return TypedResults.NotFound();

	return TypedResults.Ok(function);
});

app.MapPost("/create", async (IMovieService movieService, Function function) =>
{
	if (function is null) return;

	var movie = await movieService.GetById(function.MovieId);
	if (movie is null) return;

	await functionsRepository.Create(function);
}).WithName("AddMovie");

app.MapPost("/edit", async (Function function) =>
{
	await functionsRepository.Update(function);
});

app.MapPost("/delete", async (Guid id) =>
{
	await functionsRepository.DeleteById(id);
});

app.Run();
