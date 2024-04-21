using Cinema.Web.Models;

namespace Cinema.Web.Services;

public interface IFunctionService
{
    Task Create(Function function);
    Task Delete(Guid id);
    Task<IEnumerable<Function>> GetAll(Guid? movieId = null);
    Task<Function> GetById(Guid? id);
    Task Update(Function movie);
}
