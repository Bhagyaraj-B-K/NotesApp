using Microsoft.EntityFrameworkCore;

namespace NotesApp.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<User> User { get; set; }
    public required DbSet<Note> Note { get; set; }
}
