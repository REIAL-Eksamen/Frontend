using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Frontend.Models;
//håndteter bookinger: hent, opret og aflys. 
namespace Frontend.Services;

public class BookingClientService
{
    private readonly HttpClient _http;

    public BookingClientService(HttpClient http)
    {
        _http = http;
    }
//henter alle bookinger for en bestemt bruger. 
    public async Task<List<BookingModel>> GetBookingsByUserId(string userId)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        return await _http.GetFromJsonAsync<List<BookingModel>>($"api/bookings/user/{userId}", options) ?? new List<BookingModel>();
    }
//true hvis aflysningen gik igennem. 
    public async Task<bool> CancelBooking(string bookingId)
    {
        var response = await _http.PutAsync($"api/bookings/{bookingId}/cancel", null);
        return response.IsSuccessStatusCode;
    }
    //true hvis booking oprettet. 
    public async Task<bool> CreateBooking(string classSessionId)
    {
        var response = await _http.PostAsJsonAsync("api/bookings", new
        {
            ClassSessionId = classSessionId
        });

        return response.IsSuccessStatusCode;
    }
}