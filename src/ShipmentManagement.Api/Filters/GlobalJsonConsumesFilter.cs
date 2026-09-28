using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShipmentManagement.Api.Filters;

public class GlobalJsonConsumesFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var method = context.HttpContext.Request.Method;
        if (method != "GET" && method != "DELETE")
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var hasConsumesAttribute =
                    controllerActionDescriptor.MethodInfo.GetCustomAttributes<ConsumesAttribute>().Any()
                    || controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<ConsumesAttribute>().Any();

                if (hasConsumesAttribute) return;
            }

            var contentType = context.HttpContext.Request.ContentType;
            if (contentType == null || !contentType.Contains("application/json"))
            {
                context.Result = new UnsupportedMediaTypeResult();
            }
        }

        await next();
    }
}