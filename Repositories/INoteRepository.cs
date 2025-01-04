using NotesApp.Models;

namespace NotesApp.Repositories;

public interface INoteRepository
{
    Task<Note> GetByIdAsync(int id);
    Task<Note> GetByIdForUserAsync(int id, int userId);
    Task<IEnumerable<Note>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Note>> GetAllAsync();
    Task<bool> CreateAsync(Note user);
    Task<bool> UpdateAsync(Note user);
    Task<bool> DeleteAsync(int id, int userId);
}
