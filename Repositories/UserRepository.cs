using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Repositories;

public class UserRepository(AppDbContext _context) : IUserRepository
{
    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.User.FindAsync(id)
            ?? throw new KeyNotFoundException($"User with ID {id} not found.");
    }

    public async Task<User?> GetByCredentialsAsync(string email, string password)
    {
        return await _context.User.FirstOrDefaultAsync(u =>
            u.Email == email && u.Password == password
        );
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<bool> CreateAsync(User user)
    {
        await _context.User.AddAsync(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _context.User.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
            return false;

        _context.User.Remove(user);
        return await _context.SaveChangesAsync() > 0;
    }
}
