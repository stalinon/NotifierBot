using System.Text.Json.Serialization;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Models.Abstractions;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Infrastructure.Models.Api;

/// <summary>
///     Получатель
/// </summary>
public sealed class Recipient : IModelWithId, IRecipientSettings
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

    /// <summary>
    ///     Внешний идентификатор
    /// </summary>
    [JsonPropertyName("external_id")]
    public string ExternalIdentifier { get; set; } = default!;

    /// <summary>
    ///     Тип получателя
    /// </summary>
    [JsonPropertyName("type")]
    public RecipientType Type { get; set; }

    /// <summary>
    ///     Название получателя
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}