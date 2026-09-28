using ShipmentManagement.Domain.Enums;
using ShipmentManagement.Domain.ValueObjects;

namespace ShipmentManagement.Domain.Entities;

public class Package
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public Guid ApplicationUserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public Address OriginAddress { get; set; } = new();
    public Address DestinationAddress { get; set; } = new();
    public Status CurrentStatus { get; set; }
    public DeliveryType DeliveryType { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<TrackingEvent> TrackingEvents { get; set; } = new List<TrackingEvent>();
    public ICollection<DeliveryAttempt> DeliveryAttempts { get; set; } = new List<DeliveryAttempt>();
}