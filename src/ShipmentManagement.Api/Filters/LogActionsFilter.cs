using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShipmentManagement.Api.Filters;

public class LogActionsFilter : IAsyncActionFilter
{
    private readonly ILogger<LogActionsFilter> _logger;

    public LogActionsFilter(ILogger<LogActionsFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("Executing action {ActionName}", context.ActionDescriptor.DisplayName);
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await next();

        stopwatch.Stop();
        _logger.LogInformation("Action {ActionName} completed in {ElapsedMilliseconds} ms",
            context.ActionDescriptor.DisplayName, stopwatch.ElapsedMilliseconds);
    }
}