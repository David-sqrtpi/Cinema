using Cinema.Services.Functions.Models;

namespace Cinema.Services.Functions.Repositories;

public class LiteFunctionsRepository(LiteFunctionsContext db) : IFunctionsRepository
{
	public Task Create(Function funcion)
	{
		throw new NotImplementedException();
	}

	public Task DeleteById(Guid ids)
	{
		throw new NotImplementedException();
	}

	public Task DeleteList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}

	public Task Update(Function function)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Function>> ReadAll()
	{
		throw new NotImplementedException();
	}

	public Task<Function> ReadById(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Function>> ReadList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}
}
