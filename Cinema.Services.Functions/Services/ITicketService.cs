using Cinema.Services.Functions.Entities;

namespace Cinema.Services.Functions.Services;

public interface ITicketService
{
    public Task<Ticket> Get(Guid? id);
}
