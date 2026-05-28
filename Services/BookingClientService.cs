using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services;

public class BookingClientService
{
    private readonly HttpClient _http;

    public BookingClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BookingModel>> GetBookingsByUserId(string userId)
    {
        return await _http.GetFromJsonAsync<List<BookingModel>>($"api/bookings/user/{userId}") ?? new List<BookingModel>();
    }

    public async Task<bool> CancelBooking(string bookingId)
    {
        var response = await _http.PutAsync($"api/bookings/{bookingId}/cancel", null);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> CreateBooking(string classSessionId)
    {
        var response = await _http.PostAsJsonAsync("api/bookings", new
        {
            ClassSessionId = classSessionId
        });

        return response.IsSuccessStatusCode;
    }
}