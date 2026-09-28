using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Application.DTOs.Packages;

public class PackageV1Dto
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public AddressDto OriginAddress { get; set; } = new();
    public AddressDto DestinationAddress { get; set; } = new();
    public Status CurrentStatus { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
}