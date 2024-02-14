namespace NotifierBot.Infrastructure.Maintenance.Enums;

/// <summary>
///     Статус окружения
/// </summary>
public enum EnvironmentStatus
{
    /// <summary>
    ///     Использование БД
    /// </summary>
    USE_DATABASE = 0,
    
    /// <summary>
    ///     Использование мока БД
    /// </summary>
    USE_MOCK = 1
}