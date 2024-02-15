using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Data.Impl.Repositories;
using NotifierBot.Data.Impl.Repositories.Mock;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Maintenance;
using NotifierBot.Infrastructure.Maintenance.Enums;

namespace NotifierBot.Data.Impl;

/// <summary>
///     Расширения <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Добавить слой доступа к данным
    /// </summary>
    public static IServiceCollection SetupDataAccessLayer(this IServiceCollection services, EnvironmentStatus mode)
    {
        switch (mode)
        {
            case EnvironmentStatus.USE_DATABASE:
                var configuration = Config.GetDatabaseConfiguration();
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
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
        
        return services.AddSingleton<IDataAccessFacade, DataAccessFacade>();
    }
}