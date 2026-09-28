namespace ShipmentManagement.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string TokenHash { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}