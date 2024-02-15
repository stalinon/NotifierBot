using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Maintenance.Configuration.Impl;

namespace NotifierBot.Infrastructure.Maintenance;

/// <summary>
///     Точка доступа в конфигурации
/// </summary>
public static class Config
{
    /// <summary>
    ///     Получить конфигурацию БД
    /// </summary>
    public static IDatabaseConfiguration GetDatabaseConfiguration()
    {
        return new DatabaseConfiguration();
    }
}