using Frontend.Models;

namespace Frontend.Services;

public class UserClientService
{
    private readonly HttpClient _http;

    public UserClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task<UserModel?> GetCurrentUser()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/users/me");

        var response = await _http.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserModel>();
    }
}