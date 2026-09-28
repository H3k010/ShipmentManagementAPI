using Microsoft.AspNetCore.Identity;
using ShipmentManagement.Domain.Entities;

namespace ShipmentManagement.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<Package> Packages { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}