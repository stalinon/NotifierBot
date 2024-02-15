namespace NotifierBot.Infrastructure.Maintenance.Configuration;

/// <summary>
///     Настройки получателя
/// </summary>
public interface IRecipientSettings
{
    /// <summary>
    ///     Внешний идентификатор
    /// </summary>
    string ExternalIdentifier { get; }
}