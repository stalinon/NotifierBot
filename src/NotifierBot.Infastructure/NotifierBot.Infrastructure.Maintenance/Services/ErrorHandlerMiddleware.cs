using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NotifierBot.Infrastructure.Maintenance.Services;

/// <summary>
/// Middleware для обработки ошибок на самом верхнем уровне
/// </summary>
public sealed class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    /// <summary>
    /// .ctor
    /// </summary>
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Invoke
    /// </summary>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, $"An unhandled exception while executing request");
        }
    }
}