using Cinema.Persistence;
using Cinema.Services.Movies.Models;

namespace Cinema.Services.Movies.Repositories;

public class LiteMoviesRepository(LiteMoviesContext db) : IMoviesRepository
{
	public async Task Create(Movie function)
	{
		await db.Create(function);
	}

	public async Task DeleteById(Guid id)
	{
		await db.DeleteById<Movie>(id);
	}

	public Task DeleteList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}

	public async Task Update(Movie function)
	{
		await db.UpdateExtension(function);
	}

	public async Task<List<Movie>> ReadAll()
	{
		return await db.Movies.ReadAll();
	}

	public async Task<Movie?> ReadById(Guid id)
	{
		return await db.Movies.ReadById(id);
	}

	public Task<IEnumerable<Movie>> ReadList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}
}
