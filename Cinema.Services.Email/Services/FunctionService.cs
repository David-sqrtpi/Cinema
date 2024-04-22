using Cinema.Services.Email.Entities;
using Cinema.Services.Email.Extensions;

namespace Cinema.Services.Email.Services;

public class FunctionService(HttpClient client, IConfiguration configuration) : IFunctionService
{
    //private readonly string _url = configuration["Urls:Functions"]!;
    //private readonly string _url = $"https://final-movie-app-apim.azure-api.net/Functions";
    //private readonly string _url = $"https://localhost:7046";

    public async Task<Function> GetById(Guid? id)
    {
        //var response = await client.GetAsync($"{_url}/details/{id}");
        var response = await client.GetAsync($"/details/{id}");
        return await response.ReadContentAs<Function>();
    }
}
