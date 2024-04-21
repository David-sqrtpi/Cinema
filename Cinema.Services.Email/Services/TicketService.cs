using Cinema.Services.Email.Entities;
using Cinema.Services.Email.Extensions;

namespace Cinema.Services.Email.Services;

public class TicketService(HttpClient client) : ITicketService
{
    private readonly string _url = $"https://final-movie-app-apim.azure-api.net/Tickets";
    //private readonly string _url = $"https://localhost:7279";

    public async Task<Ticket> GetById(Guid? id = null)
    {
        var response = await client.GetAsync($"{_url}/details/{id}");
        return await response.ReadContentAs<Ticket>();
    }
}
