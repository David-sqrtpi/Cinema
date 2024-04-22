
using Cinema.Services.Functions.Entities;
using Cinema.Services.Functions.Extensions;

namespace Cinema.Services.Functions.Services;

public class TicketService(HttpClient client) : ITicketService
{
    //private readonly string _url = $"https://final-movie-app-apim.azure-api.net/Tickets";
    //private readonly string _url = $"https://localhost:7279";

    public async Task<Ticket> Get(Guid? id = null)
    {
        //var response = await client.GetAsync($"{_url}/details/{id}");
        var response = await client.GetAsync($"/details/{id}");
        return await response.ReadContentAs<Ticket>();
    }
}
