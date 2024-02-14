namespace NotifierBot.Configuration;

/// <summary>
///     Настройки бота
/// </summary>
public interface IBotSettings
{
    /// <summary>
    ///     Ключ бота
    /// </summary>
    string Key { get; }
}