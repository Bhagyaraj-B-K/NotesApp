using System.Security.Claims;
using NotesApp.Models;

namespace NotesApp.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
    Task<User> ValidateAndGetUserDetails(ClaimsPrincipal User);
    Task<User?> GetUserByCredentialsAsync(string email, string password);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}
