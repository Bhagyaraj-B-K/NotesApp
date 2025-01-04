using System.Security.Claims;
using NotesApp.Models;
using NotesApp.Repositories;

namespace NotesApp.Services;

public class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByCredentialsAsync(string email, string password)
    {
        // Must hash password before passing to repository
        // But we're not doing that here for now
        return await _userRepository.GetByCredentialsAsync(email, password);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<User> ValidateAndGetUserDetails(ClaimsPrincipal User)
    {
        try
        {
            int userId = Convert.ToInt32(User.FindFirst("id")?.Value);
            return await GetUserByIdAsync(userId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
