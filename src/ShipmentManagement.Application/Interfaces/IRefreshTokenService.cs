using ShipmentManagement.Domain.Entities;

namespace ShipmentManagement.Application.Interfaces;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    string HashRefreshToken(string refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string tokenHash);
    Task<RefreshToken?> GetRefreshTokenWithUserIdAsync(Guid userId, string tokenHash);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<bool> RevokeRefreshTokenAsync(RefreshToken refreshToken);
}