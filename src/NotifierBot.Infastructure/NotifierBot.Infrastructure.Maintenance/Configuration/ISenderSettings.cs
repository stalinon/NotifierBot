namespace NotifierBot.Infrastructure.Maintenance.Configuration;

/// <summary>
///     Настройки отправителя
/// </summary>
public interface ISenderSettings
{
    /// <summary>
    ///     Токен доступа
    /// </summary>
    string Token { get; }
}