using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShipmentManagement.Application.Validators.Packages;

namespace ShipmentManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreatePackageV1DtoValidator>();

        return services;
    }
}