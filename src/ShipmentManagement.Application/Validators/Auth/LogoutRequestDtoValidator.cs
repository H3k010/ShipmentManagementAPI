using FluentValidation;
using ShipmentManagement.Application.DTOs.Auth;

namespace ShipmentManagement.Application.Validators.Auth;

public class LogoutRequestDtoValidator : AbstractValidator<LogoutRequestDto>
{
    public LogoutRequestDtoValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}