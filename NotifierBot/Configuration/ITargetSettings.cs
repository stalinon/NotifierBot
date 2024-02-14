namespace NotifierBot.Configuration;

/// <summary>
///     Настройки подключения канала
/// </summary>
public interface ITargetSettings : IBotSettings
{
    /// <summary>
    ///     Идентификатор канала/чата
    /// </summary>
    string TargetId { get; }
}