using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services;

public class ClassClientService
{
    private readonly HttpClient _http;

    public ClassClientService(HttpClient http)
    {
        _http = http;
    }
    
    // Liste af alle hold hentes
    public async Task<List<ClassModel>> GetClasses()
    {
        return await _http.GetFromJsonAsync<List<ClassModel>>("api/class");
    }
}