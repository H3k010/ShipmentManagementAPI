namespace ShipmentManagement.Application.DTOs.Auth;

public class AuthResultDto
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}