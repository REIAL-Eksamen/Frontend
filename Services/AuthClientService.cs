using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services;

public class AuthClientService
{
    private readonly HttpClient _http;
    
    public  AuthClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string?> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("Auth/login", new LoginModel
        {
            Email = email,
            Password = password
        });

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return result?.Token;
    }

    public async Task<bool> Register(string email, string password)
    {
        var model = new RegisterModel
        {
            Email = email,
            Password = password
        };
        
        var response = await _http.PostAsJsonAsync("Auth/register", model);
        
        return response.IsSuccessStatusCode;
    }
}

public class LoginResponse
{
    public string? Token { get; set; }
}