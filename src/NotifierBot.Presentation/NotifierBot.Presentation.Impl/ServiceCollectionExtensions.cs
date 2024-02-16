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
    public static IServiceCollection AddAuthService(this IServiceCollection services)
    {
        return services.AddScoped<IAuthService, AuthService>();
    }
}