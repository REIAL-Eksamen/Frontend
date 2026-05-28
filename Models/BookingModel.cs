using System.Text.Json.Serialization;

namespace Frontend.Models;

public enum BookingStatus
{
    Confirmed,
    Cancelled,
    NoShow,
    WaitListed
}
public class BookingModel
{
    public string? ClassBookingId { get; set; }
    public string UserId { get; set; } = "";
    public string ClassSessionId { get; set; } = "";
    public DateTime BookedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookingStatus Status { get; set; }
}