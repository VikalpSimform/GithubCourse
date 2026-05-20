using System.ComponentModel.DataAnnotations;
using GithubCourse.Middleware;
using GithubCourse.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// In-memory static user list
var users = new List<User>
{
	new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
	new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
};

// Helper for validation
static (bool IsValid, IEnumerable<string> Errors) Validate(object model)
{
	var ctx = new ValidationContext(model);
	var results = new List<ValidationResult>();
	var ok = Validator.TryValidateObject(model, ctx, results, true);
	return (ok, results.Select(r => r.ErrorMessage ?? ""));
}

app.MapGet("/", () => Results.Json(new { success = true, data = "Simple Users API" }));

app.MapGet("/users", () => Results.Json(new { success = true, data = users }));

app.MapGet("/users/{id:int}", (int id) =>
{
	var user = users.FirstOrDefault(u => u.Id == id);
	if (user is null)
		return Results.Json(new { success = false, error = new { code = 404, message = "User not found" } }, statusCode: 404);

	return Results.Json(new { success = true, data = user });
});

app.MapPost("/users", async (CreateUserDto dto) =>
{
	var (isValid, errors) = Validate(dto);
	if (!isValid)
	{
		return Results.Json(new { success = false, error = new { code = 400, message = "Validation failed", details = errors } }, statusCode: 400);
	}

	var nextId = users.Any() ? users.Max(u => u.Id) + 1 : 1;
	var user = new User { Id = nextId, Name = dto.Name, Email = dto.Email };
	users.Add(user);

	return Results.Json(new { success = true, data = user }, statusCode: 201);
});

app.Run();

