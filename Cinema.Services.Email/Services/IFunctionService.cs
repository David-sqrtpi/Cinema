using Cinema.Services.Email.Entities;

namespace Cinema.Services.Email.Services;

public interface IFunctionService
{
    Task<Function> GetById(Guid? id);
}
