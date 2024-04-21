using Cinema.Web.Models;

namespace Cinema.Web.Services;

public interface ITicketService
{
    public Task<bool> Create(Ticket ticket);
}
