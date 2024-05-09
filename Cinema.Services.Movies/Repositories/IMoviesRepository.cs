using Cinema.Services.Movies.Models;

namespace Cinema.Services.Movies.Repositories;

public interface IMoviesRepository
{
	public Task<List<Movie>> ReadAll();
	public Task<IEnumerable<Movie>> ReadList(IEnumerable<Guid> ids);
	public Task<Movie?> ReadById(Guid id);
	public Task Create(Movie function);
	public Task Update(Movie function);
	public Task DeleteList(IEnumerable<Guid> ids);
	public Task DeleteById(Guid id);
}
