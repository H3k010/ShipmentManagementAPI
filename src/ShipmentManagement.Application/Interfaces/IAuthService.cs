using ShipmentManagement.Application.DTOs.Auth;

namespace ShipmentManagement.Application.Interfaces;

/// <summary>
/// Defines the contract for authentication services, including user registration, login, logout, and token refresh operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user with the provided registration details and returns an authentication result containing registration information.
    /// </summary>
    /// <param name="dto">The registration request DTO.</param>
    /// <returns>The authentication result DTO containing registration information.</returns>
    Task<AuthResultDto> RegisterAsync(RegisterRequestDto dto);

    /// <summary>
    /// Authenticates a user with the provided login credentials and returns an authentication result containing the tokens.
    /// </summary>
    /// <param name="dto">The login request DTO.</param>
    /// <returns>The authentication result DTO containing the access and refresh tokens.</returns>
    Task<TokenResponseDto?> LoginAsync(LoginRequestDto dto);

    /// <summary>
    /// Logs out a user by invalidating the provided refresh token and returns a boolean indicating the success of the logout operation.
    /// </summary>
    /// <param name="refreshToken">The refresh token to invalidate.</param>
    /// <returns>A boolean indicating the success of the logout operation.</returns>
    Task<bool> LogoutAsync(string refreshToken);

    /// <summary>
    /// Refreshes the access token using the provided refresh token and returns a new authentication result containing updated tokens.
    /// </summary>
    /// <param name="dto">The refresh request DTO.</param>
    /// <returns>The authentication result DTO containing updated access and refresh tokens.</returns>
    Task<TokenResponseDto?> RefreshAsync(RefreshRequestDto dto);
}