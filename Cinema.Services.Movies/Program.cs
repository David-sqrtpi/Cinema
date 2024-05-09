using Cinema.Services.Movies.Enums;
using Cinema.Services.Movies.Models;
using Cinema.Services.Movies.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddServer(new OpenApiServer { Url = "/" });
    x.AddServer(new OpenApiServer { Url = "https://final-movies.azurewebsites.net" });
});

builder.Services.AddDbContext<MoviesContext>();
builder.Services.AddDbContext<LiteMoviesContext>();

var databaseProvider = builder.Configuration["DatabaseProvider"];

_ = databaseProvider switch
{
	nameof(DatabaseProviders.SqlServer)
		=> builder.Services.AddTransient<IMoviesRepository, MoviesRepository>(),

	_ => builder.Services.AddTransient<IMoviesRepository, LiteMoviesRepository>()
};

var app = builder.Build();

using var scope = app.Services.CreateScope();

var moviesRepository = scope.ServiceProvider.GetRequiredService<IMoviesRepository>();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (Guid[]? ids) =>
{
    return await moviesRepository.ReadAll();
});

app.MapGet("/details/{movieId}", async Task<Results<Ok<Movie>, NotFound>> (Guid movieId) =>
{
    var movie = await moviesRepository.ReadById(movieId);

    if (movie is null) return TypedResults.NotFound();

    return TypedResults.Ok(movie);
});

app.MapPost("/create", async (Movie movie) =>
{
    await moviesRepository.Create(movie);
}).WithName("AddMovie");

app.MapPost("/edit", async (Movie movie) =>
{
    await moviesRepository.Update(movie);
});

app.MapPost("/delete", async (Guid movieId) =>
{
    await moviesRepository.DeleteById(movieId);
});

app.Run();