using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShipmentManagement.Application.Interfaces;

namespace ShipmentManagement.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AnonymousOnlyAttribute : Attribute, IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var service = serviceProvider.GetRequiredService<ICurrentUserService>();
        return new AnonymousOnlyFilter(service);
    }

}

public class AnonymousOnlyFilter : IAuthorizationFilter
{
    private readonly ICurrentUserService _currentUserService;

    public AnonymousOnlyFilter(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_currentUserService.IsAuthenticated)
        {
            context.Result = new ObjectResult(new ProblemDetails
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = "This endpoint is restricted to unauthenticated guests only.",
            })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}