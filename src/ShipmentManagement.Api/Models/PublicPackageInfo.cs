using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Api.Models;

public class PublicPackageInfo
{
    public string TrackingNumber { get; set; } = string.Empty;
    public Status CurrentStatus { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
}