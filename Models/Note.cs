namespace NotesApp.Models;

public class Note
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}
