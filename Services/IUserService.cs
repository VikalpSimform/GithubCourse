using GithubCourse.Models;

namespace GithubCourse.Services;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User CreateUser(CreateUserDto dto);
}
