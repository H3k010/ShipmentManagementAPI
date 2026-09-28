using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Application.DTOs.Packages;

public class CreatePackageV2Dto
{
    public string Name { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public AddressDto OriginAddress { get; set; } = new();
    public AddressDto DestinationAddress { get; set; } = new();
    public DeliveryType DeliveryType { get; set; }
}