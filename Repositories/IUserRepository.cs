using NotesApp.Models;

namespace NotesApp.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User?> GetByCredentialsAsync(string email, string password);
    Task<IEnumerable<User>> GetAllAsync();
    Task<bool> CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}
