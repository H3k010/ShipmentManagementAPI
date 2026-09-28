namespace ShipmentManagement.Application.DTOs.Facilities;

public class UpdateFacilityDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}