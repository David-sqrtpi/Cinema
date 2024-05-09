using Cinema.Services.Ticket.Models;
using Cinema.Persistence;

namespace Cinema.Services.Ticket.Repository;

public class LiteTicketRepository(LiteTicketsContext db) : ITicketRepository
{
	public async Task Create(Models.Ticket function)
	{
		await db.Create(function);
	}

	public async Task DeleteById(Guid id)
	{
		await db.DeleteById<Models.Ticket>(id);
	}

	public Task DeleteList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}

	public async Task Update(Models.Ticket function)
	{
		await db.UpdateExtension(function);
	}

	public async Task<List<Models.Ticket>> ReadAll()
	{
		return await db.Tickets.ReadAll();
	}

	public async Task<Models.Ticket?> ReadById(Guid id)
	{
		return await db.Tickets.ReadById(id);
	}

	public Task<IEnumerable<Models.Ticket>> ReadList(IEnumerable<Guid> ids)
	{
		throw new NotImplementedException();
	}
}
