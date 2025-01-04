using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using NotesApp.Services;

namespace NotesApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class NotesController(INoteService _noteService, IUserService _userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
        var user = await _userService.ValidateAndGetUserDetails(User);
        var notes = await _noteService.GetUserNotesAsync(user.Id);
        return Ok(notes.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateNote([FromBody] NoteRequest note)
    {
        var user = await _userService.ValidateAndGetUserDetails(User);
        Note noteData = new()
        {
            Title = note.Title,
            Content = note.Content,
            UserId = user.Id,
        };
        return await _noteService.CreateNoteAsync(noteData);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateNote([FromBody] NoteUpdateRequest note)
    {
        var user = await _userService.ValidateAndGetUserDetails(User);
        Note noteData = new()
        {
            Id = note.Id,
            UserId = user.Id,
            Title = note.Title,
            Content = note.Content,
        };

        return await _noteService.UpdateNoteAsync(noteData);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteNote(int id)
    {
        var user = await _userService.ValidateAndGetUserDetails(User);
        return await _noteService.DeleteNoteAsync(id, user.Id);
    }

    public class NoteRequest
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class NoteUpdateRequest : NoteRequest
    {
        public int Id { get; set; }
    }
}
