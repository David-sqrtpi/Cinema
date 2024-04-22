using Cinema.Services.Email.Entities;
using Cinema.Services.Email.Extensions;

namespace Cinema.Services.Email.Services;

public class MovieService(HttpClient client) : IMovieService
{
    //private readonly string _url = $"https://final-movie-app-apim.azure-api.net/Movies";
    //private readonly string _url = $"https://localhost:7253";

    public async Task<Movie> GetById(Guid? id)
    {
        //var response = await client.GetAsync($"{_url}/details/{id}");
        var response = await client.GetAsync($"/details/{id}");
        return await response.ReadContentAs<Movie>();
    }
}
