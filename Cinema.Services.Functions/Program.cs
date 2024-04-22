using Cinema.Services.Functions.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Cinema.Services.Functions.Consumers;
using Cinema.Services.Functions.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddServer(new OpenApiServer { Url = "/" });
    x.AddServer(new OpenApiServer { Url = "https://final-functions.azurewebsites.net" });
});

builder.Services.AddDbContext<FunctionsContext>();
builder.Services.AddHttpClient<IMovieService, MovieService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Movies"]!); });
builder.Services.AddHttpClient<ITicketService, TicketService>(c => { c.BaseAddress = new Uri(builder.Configuration["Urls:Tickets"]!); });

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<VerifyAvailabilityConsumer>();
	x.AddConsumer<TicketPurchasedConsumer>();

	x.UsingAzureServiceBus((context, cfg) =>
	{
		cfg.Host(builder.Configuration["ConnectionStrings:ServiceBus"]);

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

app.MapGet("/", async (FunctionsContext db, Guid? movieId) =>
{
	if(movieId is null)
		return await db.Functions.ToListAsync();

	return await db.Functions
		.Where(x => x.MovieId == movieId)
		.ToListAsync();
});

app.MapGet("/details/{functionId}", async Task<Results<Ok<Function>, NotFound>> (FunctionsContext db, Guid functionId) =>
{
	var function = await db.Functions.Where(x => x.FunctionId == functionId).FirstOrDefaultAsync();

	if (function == null) return TypedResults.NotFound();

	return TypedResults.Ok(function);
});

app.MapPost("/create", async (FunctionsContext db, IMovieService movieService, Function function) =>
{
	var movie = await movieService.GetById(function.MovieId);
	if (movie is null)
		return;

	db.Functions.Add(function);
	await db.SaveChangesAsync();
}).WithName("AddMovie");

app.MapPost("/edit", async (FunctionsContext db, Function function) =>
{
	db.Functions.Update(function);
	await db.SaveChangesAsync();
});

app.MapPost("/delete", async (FunctionsContext db, Guid functionId) =>
{
	var function = await db.Functions.FindAsync(functionId);

	if (function is not null)
		db.Functions.Remove(function);

	await db.SaveChangesAsync();
});


app.MapGet("/GetByMovieId", async (FunctionsContext db, Guid movieId) =>
{
	var ticket = await db.Functions
		.Where(x => x.MovieId == movieId)
		.ToListAsync();
});

app.Run();
