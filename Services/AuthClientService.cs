using System.Net.Http.Json;
using Frontend.Models;

//håndterer login og registrering og gemmer token når bruger logger ind. 
namespace Frontend.Services;

public class AuthClientService
{
    private readonly HttpClient _http;
    private readonly TokenProvider _tokenProvider;
    
    public AuthClientService(HttpClient http, TokenProvider tokenProvider)
    {
        _http = http;
        _tokenProvider = tokenProvider;
    }
//logger bruger ind og gemmer jwt token, så det kan bruges i efterfølgende kald. 
    public async Task<string?> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("login", new LoginModel
        {
            Email = email,
            Password = password
        });
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Login failed: {(int)response.StatusCode} {response.ReasonPhrase}");
            Console.WriteLine(error);
            return null;
        }
        
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result?.Token is null)
            return null;
        
        Console.WriteLine("=== JWT TOKEN ===");
        Console.WriteLine(result.Token);
        Console.WriteLine("=================");

        _tokenProvider.Token = result.Token;

        return result.Token;
    }

    public async Task<bool> Register(string email, string password, string firstname, string lastname, string phonenumber, MembershipType membership, MembershipStatus status)
    {
        var model = new RegisterModel
        {
            Email = email,
            Password = password,
            FirstName = firstname,
            LastName = lastname,
            PhoneNumber = phonenumber,
            Membership = membership,
            MembershipStatus = status
        };
        
        var response = await _http.PostAsJsonAsync("register", model);
        
        return response.IsSuccessStatusCode;
    }
}

public class LoginResponse
{
    public string? Token { get; set; }
}