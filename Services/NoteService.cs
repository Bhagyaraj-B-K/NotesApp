using NotesApp.Models;
using NotesApp.Repositories;

namespace NotesApp.Services;

public class NoteService(INoteRepository _noteRepository) : INoteService
{
    public async Task<Note> GetNoteByIdAsync(int id)
    {
        return await _noteRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Note>> GetUserNotesAsync(int userId)
    {
        return await _noteRepository.GetByUserIdAsync(userId);
    }

    public async Task<bool> CreateNoteAsync(Note note)
    {
        return await _noteRepository.CreateAsync(note);
    }

    public async Task<bool> UpdateNoteAsync(Note note)
    {
        try
        {
            var n = await _noteRepository.GetByIdForUserAsync(note.Id, note.UserId);
            n.Title = note.Title;
            n.Content = note.Content;
            return await _noteRepository.UpdateAsync(n);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteNoteAsync(int id, int userId)
    {
        return await _noteRepository.DeleteAsync(id, userId);
    }
}
