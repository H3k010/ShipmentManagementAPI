using ShipmentManagement.Domain.Enums;

namespace ShipmentManagement.Domain.Entities;

public class DeliveryAttempt
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public DateTime AttemptedAt { get; set; }
    public Result DeliveryResult { get; set; }
    public string? FailureReason { get; set; }
    public Package Package { get; set; } = null!;
}