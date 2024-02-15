namespace NotifierBot.Infrastructure.Maintenance.Configuration;

/// <summary>
///     Настройки получателя/отправителя
/// </summary>
public interface ISenderRecipientSettings : ISenderSettings, IRecipientSettings
{
    /// <summary>
    ///     Слить в общие настройки
    /// </summary>
    static ISenderRecipientSettings Merge(ISenderSettings sender, IRecipientSettings recipient) =>
        new SenderRecipientSettings(sender.Token, recipient.ExternalIdentifier);

    private sealed record SenderRecipientSettings(string Token, string ExternalIdentifier) : ISenderRecipientSettings;
}