using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Application.DTOs.TrackingEvents;

public class TrackingEventResult
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public string? FacilityName { get; set; }
    public Status Status { get; set; }
    public DateTime OccuredAt { get; set; }
    public string? Description { get; set; }
}