using NotesApp.Repositories;
using NotesApp.Services;

namespace NotesApp.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<INoteRepository, NoteRepository>();
        return services;
    }
}
