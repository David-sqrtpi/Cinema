namespace Cinema.Services.Ticket.Repository;

public interface ITicketRepository
{
	public Task<List<Models.Ticket>> ReadAll();
	public Task<IEnumerable<Models.Ticket>> ReadList(IEnumerable<Guid> ids);
	public Task<Models.Ticket?> ReadById(Guid id);
	public Task Create(Models.Ticket function);
	public Task Update(Models.Ticket function);
	public Task DeleteList(IEnumerable<Guid> ids);
	public Task DeleteById(Guid id);
}
