using FluentValidation;
using ShipmentManagement.Application.DTOs.Facilities;

namespace ShipmentManagement.Application.Validators.Facilities;

public class UpdateFacilityDtoValidator : AbstractValidator<UpdateFacilityDto>
{
    public UpdateFacilityDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Facility name is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
    }
}