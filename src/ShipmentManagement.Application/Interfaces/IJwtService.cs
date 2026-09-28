using System.Security.Claims;
using ShipmentManagement.Application.DTOs.Auth;

namespace ShipmentManagement.Application.Interfaces;

public interface IJwtService
{
    JwtTokenDto GenerateAccessToken(JwtUserDto user);
    ClaimsPrincipal? GetPrincipleFromExpiredToken(string expiredToken);
}