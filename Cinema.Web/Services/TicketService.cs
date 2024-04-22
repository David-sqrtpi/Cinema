using Cinema.Web.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cinema.Web.Services;

public class TicketService(HttpClient client) : ITicketService
{
    //private readonly string _url = "https://final-movie-app-apim.azure-api.net/Tickets";
    //private readonly string _url = "https://localhost:7279";
    public async Task<bool> Create(Ticket ticket)
    {
        //var result = await client.PostAsJsonAsync($"{_url}/create", ticket);
        var result = await client.PostAsJsonAsync($"/create", ticket);

        return result.IsSuccessStatusCode;
    }
}
