using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Presentation.Impl.Services;
using NotifierBot.Presentation.Services;

namespace NotifierBot.Presentation.Impl;

/// <summary>
///     Расширения <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Добавить сервисы авторизации
    /// </summary>
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        return services.AddScoped<ILocalStorage, LocalStorage>();
    }
}