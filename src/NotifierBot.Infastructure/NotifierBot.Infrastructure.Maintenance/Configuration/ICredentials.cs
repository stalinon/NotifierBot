namespace NotifierBot.Infrastructure.Maintenance.Configuration;

/// <summary>
///     Креды пользователя
/// </summary>
public interface ICredentials
{
    /// <summary>
    ///     Логин
    /// </summary>
    string Login { get; }
    
    /// <summary>
    ///     Пароль
    /// </summary>
    string? Password { get; }
}