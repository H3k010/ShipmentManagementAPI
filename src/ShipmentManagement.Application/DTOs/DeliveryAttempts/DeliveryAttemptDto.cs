namespace ShipmentManagement.Application.DTOs.DeliveryAttempts;

public class DeliveryAttemptDto
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public DateTime AttemptedAt { get; set; }
    public string DeliveryResult { get; set; } = string.Empty;
    public string? FailureReason { get; set; }
}