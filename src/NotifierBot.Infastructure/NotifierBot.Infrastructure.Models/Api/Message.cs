using System.Text.Json.Serialization;
using NotifierBot.Infrastructure.Models.Abstractions;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Infrastructure.Models.Api;

/// <inheritdoc cref="IMessage" />
public sealed class Message : IMessage, IModelWithId
{
    /// <summary>
    ///     Идентификатор в БД
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    
    /// <summary>
    ///     Дата создания записи
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    /// <summary>
    ///     Дата обновления записи
    /// </summary>
    [JsonPropertyName("updated")]
    public DateTime Updated { get; set; }
    
    /// <inheritdoc />
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    /// <inheritdoc />
    [JsonPropertyName("attachment")]
    public byte[]? Attachment { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("attachment_type")]
    public AttachmentType? AttachmentType { get; set; }

    /// <summary>
    ///     Идентификатор отправителя
    /// </summary>
    [JsonPropertyName("sender_id")]
    public long SenderId { get; set; }

    /// <summary>
    ///     Идентификатор получателя
    /// </summary>
    [JsonPropertyName("recipient_id")]
    public long RecipientId { get; set; }
}