namespace ShipmentManagement.Application.DTOs.TrackingEvents;

public class RecordEventResult
{
    public bool IsSuccess { get; set; }
    public TrackingEventResult TrackingEventResult { get; set; } = new();
    public string? Message { get; set; }
}