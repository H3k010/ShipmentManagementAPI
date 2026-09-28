using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ShipmentManagement.Application.Interfaces;

namespace ShipmentManagement.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid? UserId
    {
        get
        {
            var stringId = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.TryParse(stringId, out var parsedId) ? parsedId : (Guid?)null;
            return id;
        }
    }

    public bool IsAuthenticated => _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}