using System.Text.Json.Serialization;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Models.Abstractions;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Infrastructure.Models.Api;

/// <summary>
///     Отправитель
/// </summary>
public class Sender : IModelWithId, ISenderSettings
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
    ///     Токен отправителя
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = default!;

    /// <summary>
    ///     Тип отправителя
    /// </summary>
    [JsonPropertyName("type")]
    public SenderType Type { get; set; }

    /// <summary>
    ///     Название отправителя
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}