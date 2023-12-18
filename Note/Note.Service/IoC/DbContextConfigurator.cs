using Note.DataAccess;
using Note.Service.Settings;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Service.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, NoteSettings settings)
    {
        services.AddDbContextFactory<NoteDbContext>(
            options => { options.UseSqlServer(settings.NoteDbContextConnectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<NoteDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate(); //makes last migrations to db and creates database if it doesn't exist
    }
}
