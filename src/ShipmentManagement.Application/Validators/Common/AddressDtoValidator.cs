using FluentValidation;
using ShipmentManagement.Application.DTOs.Common;

namespace ShipmentManagement.Application.Validators.Common;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.").MaximumLength(100)
            .WithMessage("Street must not exceed 100 characters.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.").MaximumLength(100)
            .WithMessage("City must not exceed 100 characters.");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal code is required.").MaximumLength(20)
            .WithMessage("Postal code must not exceed 20 characters.");
    }
}