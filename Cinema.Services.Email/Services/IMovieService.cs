using Cinema.Services.Email.Entities;

namespace Cinema.Services.Email.Services;

public interface IMovieService
{
    Task<Movie> GetById(Guid? id);
}
