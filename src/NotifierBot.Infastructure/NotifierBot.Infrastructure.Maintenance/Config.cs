using Microsoft.AspNetCore.Authentication.Cookies;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Maintenance.Configuration.Impl;

namespace NotifierBot.Infrastructure.Maintenance;

/// <summary>
///     Точка доступа в конфигурации
/// </summary>
public static class Config
{
    /// <summary>
    ///     Схема аутентификации
    /// </summary>
    public const string AUTH_SCHEME = CookieAuthenticationDefaults.AuthenticationScheme;
    
    /// <summary>
    ///     Получить конфигурацию БД
    /// </summary>
    public static IDatabaseConfiguration GetDatabaseConfiguration()
    {
        return new DatabaseConfiguration();
    }
    
    /// <summary>
    ///     Получить креды супер админа
    /// </summary>
    public static ICredentials GetSuperAdminCredentials()
    {
        return new SuperAdminCredentials();
    }
}