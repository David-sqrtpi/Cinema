using Cinema.Services.Email.Entities;

namespace Cinema.Services.Email.Services;

public interface ITicketService
{
    public Task<Ticket> GetById(Guid? id);
}
