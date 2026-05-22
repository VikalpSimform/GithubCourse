using Microsoft.AspNetCore.Http.HttpResults;

namespace GithubCourse.Endpoints;

public static class DayEndpoints
{
    public static IEndpointRouteBuilder MapDayEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/days")
            .WithTags("Days")
            .WithOpenApi();

        group.MapGet("/{number:int}", GetDay)
            .WithName("GetDayByNumber")
            .WithSummary("Get day name by number (1-7)")
            .Produces<ApiResponse<string>>(StatusCodes.Status200OK)
            .Produces<ApiErrorResponse>(StatusCodes.Status400BadRequest);

        return app;
    }

    private static Results<Ok<ApiResponse<string>>, BadRequest<ApiErrorResponse>> GetDay(int number)
    {
        if (number < 1 || number > 7)
        {
            return TypedResults.BadRequest(new ApiErrorResponse(400, "Day number must be between 1 and 7"));
        }

        var days = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        var day = days[number - 1];

        return TypedResults.Ok(new ApiResponse<string>(day));
    }
}
