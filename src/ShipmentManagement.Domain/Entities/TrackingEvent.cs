using ShipmentManagement.Domain.Enums;

namespace ShipmentManagement.Domain.Entities;

public class TrackingEvent
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public int? FacilityId { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public string? FacilityName { get; set; }
    public Status Status { get; set; }
    public DateTime OccuredAt { get; set; }
    public string? Description { get; set; }
    public Package Package { get; set; } = null!;
    public Facility? Facility { get; set; }
}