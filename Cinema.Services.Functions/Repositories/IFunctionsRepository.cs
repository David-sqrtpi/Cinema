using Cinema.Services.Functions.Models;

namespace Cinema.Services.Functions.Repositories;

public interface IFunctionsRepository
{
	public Task<IEnumerable<Function>> ReadAll();
	public Task<IEnumerable<Function>> ReadList(IEnumerable<Guid> ids);
	public Task<Function> ReadById(Guid id);
	public Task Create(Function funcion);
	public Task Update(Function function);
	public Task DeleteList(IEnumerable<Guid> ids);
	public Task DeleteById(Guid ids);
}
