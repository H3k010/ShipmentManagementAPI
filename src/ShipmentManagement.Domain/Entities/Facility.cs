namespace ShipmentManagement.Domain.Entities;

public class Facility
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<TrackingEvent> TrackingEvents { get; set; } = new List<TrackingEvent>();
}