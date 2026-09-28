namespace ShipmentManagement.Application.DTOs.Auth;

public class JwtUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IEnumerable<string> Roles { get; set; } = [];
}