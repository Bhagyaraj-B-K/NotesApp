using Microsoft.EntityFrameworkCore;
using NotesApp.Models;

namespace NotesApp.Repositories;

public class NoteRepository(AppDbContext _context) : INoteRepository
{
    public async Task<Note> GetByIdAsync(int id)
    {
        return await _context.Note.FindAsync(id)
            ?? throw new KeyNotFoundException($"Note with ID {id} not found.");
    }

    public async Task<Note> GetByIdForUserAsync(int id, int userId)
    {
        var note = await _context.Note.FindAsync(id);
        if (note == null || note.UserId != userId)
            throw new KeyNotFoundException($"Note with ID {id} not found.");
        return note;
    }

    public async Task<IEnumerable<Note>> GetByUserIdAsync(int userId)
    {
        return await _context.Note.Where(n => n.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await _context.Note.ToListAsync();
    }

    public async Task<bool> CreateAsync(Note note)
    {
        await _context.Note.AddAsync(note);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Note note)
    {
        _context.Note.Update(note);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var note = await _context.Note.FindAsync(id);
        if (note == null || note.UserId != userId)
            return false;

        _context.Note.Remove(note);
        return await _context.SaveChangesAsync() > 0;
    }
}
