using GithubCourse.Models;

namespace GithubCourse.Services;

public class UserService : IUserService
{
    private readonly List<User> _users;
    private readonly ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
        
        // Initialize with seed data
        _users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };
    }

    public IEnumerable<User> GetAllUsers()
    {
        _logger.LogInformation("Retrieving all users");
        return _users.AsReadOnly();
    }

    public User? GetUserById(int id)
    {
        _logger.LogInformation("Retrieving user with ID: {UserId}", id);
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(CreateUserDto dto)
    {
        var nextId = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        var user = new User 
        { 
            Id = nextId, 
            Name = dto.Name, 
            Email = dto.Email 
        };
        
        _users.Add(user);
        _logger.LogInformation("Created new user with ID: {UserId}", user.Id);
        
        return user;
    }
}
