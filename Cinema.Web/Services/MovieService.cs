using Cinema.Web.Extensions;
using Cinema.Web.Models;

namespace Cinema.Web.Services;

public class MovieService(HttpClient httpClient) : IMovieService
{
    //private readonly string _url = $"https://final-movie-app-apim.azure-api.net/Movies";
    //private readonly string _url = $"https://localhost:7253";

    public async Task Create(Movie movie)
    {
        //await httpClient.PostAsJsonAsync($"{_url}/create", movie);
        await httpClient.PostAsJsonAsync($"/create", movie);
    }

    public async Task Delete(Guid id)
    {
#warning It is working, but investigate the request body
        //await httpClient.PostAsJsonAsync($"{_url}/delete?movieId={id}", id);
        await httpClient.PostAsJsonAsync($"/delete?movieId={id}", id);
    }

    //public async Task<IEnumerable<Movie>> GetAll()
    //{
    //    var response = await httpClient.GetAsync($"{_url}");
    //    return await response.ReadContentAs<IEnumerable<Movie>>();
    //}

    public async Task<IEnumerable<Movie>> GetAll(IEnumerable<Guid>? ids = null)
    {
        var idsQuery = string.Empty;
        if (ids?.Any() ?? false)
            idsQuery = "ids=" + string.Join("&ids=", ids);

        //var response = await httpClient.GetAsync($"{_url}?{idsQuery}");
        var response = await httpClient.GetAsync($"?{idsQuery}");

        return await response.ReadContentAs<IEnumerable<Movie>>();
    }

    public async Task<Movie> GetById(Guid? id)
    {
        //var response = await httpClient.GetAsync($"{_url}/details/{id}");
        var response = await httpClient.GetAsync($"/details/{id}");
        return await response.ReadContentAs<Movie>();
    }

    public async Task Update(Movie movie)
    {
        //await httpClient.PostAsJsonAsync($"{_url}/edit", movie);
        await httpClient.PostAsJsonAsync($"/edit", movie);
    }
}
