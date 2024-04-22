using Cinema.Web.Models;

namespace Cinema.Web.Services;

public interface IMovieService
{
    Task Create(Movie movie);
    Task Delete(Guid id);
    Task<IEnumerable<Movie>> GetAll(IEnumerable<Guid>? ids = null);
    Task<Movie> GetById(Guid? id);
    Task Update(Movie movie);
}
