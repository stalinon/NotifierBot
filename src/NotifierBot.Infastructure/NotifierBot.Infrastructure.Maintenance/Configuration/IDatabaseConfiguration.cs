using NotifierBot.Infrastructure.Maintenance.Enums;

namespace NotifierBot.Infrastructure.Maintenance.Configuration;

/// <summary>
///     Настройки подключения к БД
/// </summary>
public interface IDatabaseConfiguration
{
    /// <summary>
    ///     Строка подключения
    /// </summary>
    string ConnectionString { get; }
    
    /// <summary>
    ///     Режим работы БД
    /// </summary>
    EnvironmentStatus DatabaseMode { get; }
}