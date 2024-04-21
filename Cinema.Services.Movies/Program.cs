using Cinema.Services.Movies.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddServer(new OpenApiServer { Url = "/" });
    x.AddServer(new OpenApiServer { Url = "https://final-movies.azurewebsites.net" });
});

builder.Services.AddDbContext<MoviesContext>();

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (MoviesContext db, Guid[]? ids) =>
{
    if (ids is null || ids.Length == 0)
        return await db.Movies.ToListAsync();

    return await db.Movies
    .Where(x => ids.Contains(x.MovieId))
    .ToListAsync();
});

app.MapGet("/details/{movieId}", async Task<Results<Ok<Movie>, NotFound>> (MoviesContext db, Guid movieId) =>
{
    var movie = await db.Movies.Where(x => x.MovieId == movieId).FirstOrDefaultAsync();

    if (movie == null) return TypedResults.NotFound();

    return TypedResults.Ok(movie);
});

app.MapPost("/create", async (MoviesContext db, Movie movie) =>
{
    db.Movies.Add(movie);
    await db.SaveChangesAsync();
}).WithName("AddMovie");

app.MapPost("/edit", async (MoviesContext db, Movie movie) =>
{
    db.Movies.Update(movie);
    await db.SaveChangesAsync();
});

app.MapPost("/delete", async (MoviesContext db, Guid movieId) =>
{
    var movie = await db.Movies.FindAsync(movieId);

    if(movie is not null)
        db.Movies.Remove(movie);

    await db.SaveChangesAsync();
});

app.Run();