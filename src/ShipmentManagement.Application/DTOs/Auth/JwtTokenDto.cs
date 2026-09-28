namespace ShipmentManagement.Application.DTOs.Auth;

public class JwtTokenDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}