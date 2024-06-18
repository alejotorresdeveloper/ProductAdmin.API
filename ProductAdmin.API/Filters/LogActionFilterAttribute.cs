namespace ProductAdmin.API.Filters;

using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

public class LogActionFilterAttribute : Attribute, IAsyncActionFilter
{
    private readonly ILogger<LogActionFilterAttribute> _logger;

    public LogActionFilterAttribute(ILogger<LogActionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await next();

        stopwatch.Stop();

        long elapsedTimeMilliseconds = stopwatch.ElapsedMilliseconds;

        _logger.LogWarning("======================= REQUEST ELAPSED TIME =======================");
        _logger.LogWarning($"{context.HttpContext.GetEndpoint()?.DisplayName ?? ""} Elapsed time: {elapsedTimeMilliseconds}ms");
        _logger.LogWarning("====================================================================");
    }
}