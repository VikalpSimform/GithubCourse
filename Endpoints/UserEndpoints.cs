using GithubCourse.Models;
using GithubCourse.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GithubCourse.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users")
            .WithTags("Users")
            .WithOpenApi();

        group.MapGet("/", GetAllUsers)
            .WithName("GetAllUsers")
            .WithSummary("Get all users")
            .Produces<ApiResponse<IEnumerable<User>>>(StatusCodes.Status200OK);

        group.MapGet("/{id:int}", GetUserById)
            .WithName("GetUserById")
            .WithSummary("Get user by ID")
            .Produces<ApiResponse<User>>(StatusCodes.Status200OK)
            .Produces<ApiErrorResponse>(StatusCodes.Status404NotFound);

        group.MapPost("/", CreateUser)
            .WithName("CreateUser")
            .WithSummary("Create a new user")
            .Produces<ApiResponse<User>>(StatusCodes.Status201Created)
            .Produces<ApiValidationErrorResponse>(StatusCodes.Status400BadRequest);

        return app;
    }

    private static Ok<ApiResponse<IEnumerable<User>>> GetAllUsers(IUserService userService)
    {
        var users = userService.GetAllUsers();
        return TypedResults.Ok(new ApiResponse<IEnumerable<User>>(users));
    }

    private static Results<Ok<ApiResponse<User>>, NotFound<ApiErrorResponse>> GetUserById(
        int id, 
        IUserService userService)
    {
        var user = userService.GetUserById(id);
        
        if (user is null)
        {
            return TypedResults.NotFound(new ApiErrorResponse(404, "User not found"));
        }

        return TypedResults.Ok(new ApiResponse<User>(user));
    }

    private static Results<Created<ApiResponse<User>>, BadRequest<ApiValidationErrorResponse>> CreateUser(
        CreateUserDto dto,
        IUserService userService,
        IValidationService validationService)
    {
        var (isValid, errors) = validationService.Validate(dto);
        
        if (!isValid)
        {
            return TypedResults.BadRequest(
                new ApiValidationErrorResponse(400, "Validation failed", errors));
        }

        var user = userService.CreateUser(dto);
        return TypedResults.Created($"/users/{user.Id}", new ApiResponse<User>(user));
    }
}

// Response DTOs
public record ApiResponse<T>(T Data, bool Success = true);

public record ApiErrorResponse(int Code, string Message, bool Success = false);

public record ApiValidationErrorResponse(
    int Code, 
    string Message, 
    IEnumerable<string> Details, 
    bool Success = false);
