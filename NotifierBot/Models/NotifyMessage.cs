namespace NotifierBot.Models;

/// <summary>
///     Сообщение-уведомление
/// </summary>
public class NotifyMessage
{
    /// <summary>
    ///     Заголовок сообщения
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    ///     екст сообщения
    /// </summary>
    public string Text { get; init; } = string.Empty;
}