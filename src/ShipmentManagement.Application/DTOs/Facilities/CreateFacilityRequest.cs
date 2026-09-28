namespace ShipmentManagement.Application.DTOs.Facilities;

public class CreateFacilityRequest
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}