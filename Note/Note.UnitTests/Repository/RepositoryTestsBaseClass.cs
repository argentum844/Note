using Note.DataAccess;
using Note.Service.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Note.UnitTests.Repository;

public class RepositoryTestsBaseClass
{
    public RepositoryTestsBaseClass()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Test.json", optional: true)
            .Build();

        Settings = NoteSettingsReader.Read(configuration);
        ServiceProvider = ConfigureServiceProvider();

        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<NoteDbContext>>();
    }

    private IServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContextFactory<NoteDbContext>(
            options => { options.UseSqlServer(Settings.NoteDbContextConnectionString); },
            ServiceLifetime.Scoped);
        return serviceCollection.BuildServiceProvider();
    }

    protected readonly NoteSettings Settings;
    protected readonly IDbContextFactory<NoteDbContext> DbContextFactory;
    protected readonly IServiceProvider ServiceProvider;
}