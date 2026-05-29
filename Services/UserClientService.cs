using Frontend.Models;

//her hentes data om den bruger der er logget ind. 
namespace Frontend.Services;

public class UserClientService
{
    private readonly HttpClient _http;

    public UserClientService(HttpClient http)
    {
        _http = http;
    }
//returnerer null hvis bruger ikke er logget ind eller ikke findes. 
    public async Task<UserModel?> GetCurrentUser()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/users/me");

        var response = await _http.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
            response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserModel>();
    }
}