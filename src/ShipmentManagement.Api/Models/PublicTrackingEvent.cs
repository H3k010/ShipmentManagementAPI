using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Api.Models;

public class PublicTrackingEvent
{
    public Status Status { get; set; }
    public DateTime OccuredAt { get; set; }
    public string? Description { get; set; }
}