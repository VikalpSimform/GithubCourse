using GithubCourse.Endpoints;
using GithubCourse.Middleware;
using GithubCourse.Services;
using GithubCourse.Models.Validators;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// after builder is created
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

// Configure services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() 
    { 
        Title = "Users API", 
        Version = "v1",
        Description = "A simple API for managing users"
    });
});

// Register application services
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IValidationService, ValidationService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure middleware pipeline
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API v1");
    });
}
else
{
    app.UseHttpsRedirection();
}

// Map endpoints
app.MapGet("/", () => TypedResults.Ok(new { message = "Simple Users API", version = "1.0" }))
    .WithName("GetRoot")
    .WithTags("Info")
    .ExcludeFromDescription();

app.MapUserEndpoints();

app.Run();

