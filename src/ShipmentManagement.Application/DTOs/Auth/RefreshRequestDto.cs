namespace ShipmentManagement.Application.DTOs.Auth;

public class RefreshRequestDto
{
    public string ExpiredAccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}