using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Data.Impl.Repositories;
using NotifierBot.Data.Impl.Repositories.Mock;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Maintenance;
using NotifierBot.Infrastructure.Maintenance.Enums;
using NotifierBot.Infrastructure.Maintenance.Exceptions;

namespace NotifierBot.Data.Impl;

/// <summary>
///     Расширения <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Добавить слой доступа к данным
    /// </summary>
    public static IServiceCollection SetupDataAccessLayer(this IServiceCollection services)
    {
        var configuration = Config.GetDatabaseConfiguration();
        switch (configuration.DatabaseMode)
        {
            case EnvironmentStatus.USE_DATABASE:
                services.AddDbContext<DatabaseContext>(option => option.UseNpgsql(configuration.ConnectionString))
                        .AddScoped<IMessageRepository, MessageRepository>()
                        .AddScoped<IScheduleRepository, ScheduleRepository>()
                        .AddScoped<IRecipientRepository, RecipientRepository>()
                        .AddScoped<ISenderRepository, SenderRepository>();
                break;
            case EnvironmentStatus.USE_MOCK:
                EnvironmentDataGenerator.GenerateIfEmpty();
                services.AddScoped<IMessageRepository, MockMessageRepository>()
                        .AddScoped<IScheduleRepository, MockScheduleRepository>()
                        .AddScoped<IRecipientRepository, MockRecipientRepository>()
                        .AddScoped<ISenderRepository, MockSenderRepository>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(configuration.DatabaseMode), configuration.DatabaseMode, null);
        }
        
        return services.AddSingleton<IDataAccessFacade, DataAccessFacade>();
    }
    
    /// <summary>
    ///     Применяет миграции
    /// </summary>
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        var configuration = Config.GetDatabaseConfiguration();
        if (configuration.DatabaseMode == EnvironmentStatus.USE_MOCK)
        {
            return;
        }
        
        try
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            db.Database.SetCommandTimeout(TimeSpan.FromDays(2));
            db.Database.Migrate();
            db.Database.SetCommandTimeout(null);
        }
        catch (Exception)
        {
            throw new ErrorException(HttpStatusCode.InternalServerError, "Error applying database migrations");
        }
    }
}