using Cinema.Web.Extensions;
using Cinema.Web.Models;

namespace Cinema.Web.Services;

public class FunctionService(HttpClient httpClient) : IFunctionService
{
    private readonly string _url = $"https://final-movie-app-apim.azure-api.net/Functions";
    //private readonly string _url = $"https://localhost:7046";
    public async Task Create(Function function)
    {
        await httpClient.PostAsJsonAsync($"{_url}/create", function);
    }

    public async Task Delete(Guid id)
    {
#warning It is working, but investigate the request body
        await httpClient.PostAsJsonAsync($"{_url}/delete?functionId={id}", id);
    }

    public async Task<IEnumerable<Function>> GetAll(Guid? movieId)
    {
		var response = await httpClient.GetAsync($"{_url}{(movieId is not null ? $"?{nameof(movieId)}={movieId}" : string.Empty)}");
        return await response.ReadContentAs<IEnumerable<Function>>();
    }

    public async Task<Function> GetById(Guid? id)
    {
        var response = await httpClient.GetAsync($"{_url}/details/{id}");
        return await response.ReadContentAs<Function>();
    }

    public async Task Update(Function function)
    {
        await httpClient.PostAsJsonAsync($"{_url}/edit", function);
    }
}
