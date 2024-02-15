using System.Text.Json.Serialization;
using NotifierBot.Infrastructure.Models.Abstractions;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Содержимое сообщения
/// </summary>
public sealed class MessageContent : IMessage
{
    /// <inheritdoc />
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    /// <inheritdoc />
    [JsonPropertyName("attachment")]
    public byte[]? Attachment { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("attachment_type")]
    public AttachmentType? AttachmentType { get; set; }
}