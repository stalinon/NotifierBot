using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Services;
using NotifierBot.Services.Impl;

namespace NotifierBot;

/// <summary>
///     Расширения <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureTelegramNotifications(this IServiceCollection services)
    {
        services.AddScoped<INotifyManager, TelegramNotifyManager>();
        return services;
    }
}