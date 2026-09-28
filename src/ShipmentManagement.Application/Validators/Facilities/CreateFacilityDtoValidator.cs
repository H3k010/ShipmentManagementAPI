using FluentValidation;
using ShipmentManagement.Application.DTOs.Facilities;

namespace ShipmentManagement.Application.Validators.Facilities;

public class CreateFacilityDtoValidator : AbstractValidator<CreateFacilityDto>
{
    public CreateFacilityDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Facility name is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
    }
}