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

    public async Task<ClassModel?> GetClassById(string classId)
    {
        var response = await _http.GetAsync($"api/class/{classId}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<ClassModel>();
    }
}