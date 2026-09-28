using FluentValidation;
using ShipmentManagement.Application.DTOs.Auth;

namespace ShipmentManagement.Application.Validators.Auth;

public class RefreshRequestDtoValidator : AbstractValidator<RefreshRequestDto>
{
    public RefreshRequestDtoValidator()
    {
        RuleFor(x => x.ExpiredAccessToken).NotEmpty().WithMessage("Expired access token is required.");
        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh token is required.");
    }
}