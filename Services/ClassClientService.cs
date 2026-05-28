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

    public async Task<List<ClassModel>> GetClasses()
    {
        return await _http.GetFromJsonAsync<List<ClassModel>>("api/class") ?? new List<ClassModel>();
    }

    //Book et hold page
    public async Task<List<ClassOverviewModel>> GetClassOverview()
    {
        var result = await _http.GetFromJsonAsync<List<ClassOverviewModel>>("api/Class/overview");
        return result ?? new List<ClassOverviewModel>();
    }
    
    public async Task AddMember(string classId)
    {
        var response = await _http.PostAsync($"api/class/{classId}/members", null);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}