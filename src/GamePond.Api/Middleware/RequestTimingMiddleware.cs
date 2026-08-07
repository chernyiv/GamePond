using System.Diagnostics;

namespace GamePond.Api.Middleware;

public sealed class RequestTimingMiddleware (RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startedAt = Stopwatch.GetTimestamp();
        
        await next(context);
        var elapsed = Stopwatch.GetElapsedTime(startedAt);
        
        logger.LogInformation(
            "HTTP {Method} {Path} returned {StatusCode} in {ElapsedMs} ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            elapsed.TotalMilliseconds);
    }
}