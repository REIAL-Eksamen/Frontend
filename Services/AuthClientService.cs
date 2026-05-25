using System.Net.Http.Json;
using Frontend.Models;

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

    public async Task<string?> Login(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("Auth/login", new LoginModel
        {
            Email = email,
            Password = password
        });

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
            Membership = MembershipType.Standard,
            MembershipStatus = MembershipStatus.Active
            
            
        };
        
        var response = await _http.PostAsJsonAsync("Auth/register", model);
        
        return response.IsSuccessStatusCode;
    }
}

public class LoginResponse
{
    public string? Token { get; set; }
}