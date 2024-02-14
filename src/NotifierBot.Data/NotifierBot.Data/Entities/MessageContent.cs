using System.Text.Json.Serialization;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Содержимое сообщения
/// </summary>
public sealed class MessageContent
{
    /// <summary>
    ///     Текст сообщения
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    /// <summary>
    ///     Приложение к сообщению
    /// </summary>
    [JsonPropertyName("attachment")]
    public byte[]? Attachment { get; set; }

    /// <summary>
    ///     Тип приложения
    /// </summary>
    [JsonPropertyName("attachment_type")]
    public AttachmentType? AttachmentType { get; set; }
}