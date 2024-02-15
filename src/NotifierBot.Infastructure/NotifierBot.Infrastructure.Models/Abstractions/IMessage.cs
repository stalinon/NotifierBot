using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Infrastructure.Models.Abstractions;

/// <summary>
///     Сообщение
/// </summary>
public interface IMessage
{
    /// <summary>
    ///     Текст сообщения
    /// </summary>
    string Text { get; }

    /// <summary>
    ///     Приложение к сообщению
    /// </summary>
    byte[]? Attachment { get; }

    /// <summary>
    ///     Тип приложения
    /// </summary>
    AttachmentType? AttachmentType { get; }
}