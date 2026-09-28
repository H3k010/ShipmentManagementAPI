using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Application.DTOs.TrackingEvents;

public class CreateTrackingEventRequest
{
    public int? FacilityId { get; set; }
    public Status NewStatus { get; set; }
    public string? Description { get; set; }
}