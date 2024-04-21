using Cinema.Services.Functions.Entities;

namespace Cinema.Services.Functions.Services;

public interface IMovieService
{
    Task<Movie> GetById(Guid? id);
}
