using NotesApp.Models;

namespace NotesApp.Services;

public interface INoteService
{
    Task<Note> GetNoteByIdAsync(int id);
    Task<IEnumerable<Note>> GetUserNotesAsync(int userId);
    Task<bool> CreateNoteAsync(Note note);
    Task<bool> UpdateNoteAsync(Note note);
    Task<bool> DeleteNoteAsync(int id, int userId);
}
