using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GithubCourse.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var env = context.RequestServices.GetService<IWebHostEnvironment>();
            var includeDetails = env is not null && env.IsDevelopment();

            var payload = new
            {
                success = false,
                error = new
                {
                    code = 500,
                    message = "An unexpected error occurred.",
                    details = includeDetails ? ex.Message : null
                }
            };

            await context.Response.WriteAsJsonAsync(payload);
        }
    }
}
