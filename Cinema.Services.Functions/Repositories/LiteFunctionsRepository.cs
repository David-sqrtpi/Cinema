using Cinema.Persistence;
using Cinema.Services.Functions.Models;

namespace Cinema.Services.Functions.Repositories;

public class LiteFunctionsRepository(LiteFunctionsContext db) : IFunctionsRepository
{
	public async Task Create(Function function)
	{
		await db.Create(function);
	}

	public async Task DeleteById(Guid id)
	{
		await db.DeleteById<Function>(id);
	}

	public Task DeleteList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}

	public async Task Update(Function function)
	{
		await db.UpdateExtension(function);
	}

	public async Task<List<Function>> ReadAll()
	{
		return await db.Functions.ReadAll();
	}

	public async Task<Function?> ReadById(Guid id)
	{
		return await db.Functions.ReadById(id);
	}

	public Task<IEnumerable<Function>> ReadList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}
}
