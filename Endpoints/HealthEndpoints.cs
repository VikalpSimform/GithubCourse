using Microsoft.AspNetCore.Http.HttpResults;

namespace GithubCourse.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", GetHealth)
            .WithName("GetHealth")
            .WithTags("Health")
            .WithSummary("Health check")
            .WithOpenApi()
            .Produces<HealthResponse>(StatusCodes.Status200OK);

        return app;
    }

    private static Ok<HealthResponse> GetHealth()
    {
        var response = new HealthResponse(
            Status: "Healthy",
            Timestamp: DateTime.UtcNow,
            Version: "1.0"
        );

        return TypedResults.Ok(response);
    }
}

public record HealthResponse(string Status, DateTime Timestamp, string Version);
