using System.Text.Json.Serialization;
using NotifierBot.Infrastructure.Models.Abstractions;

namespace NotifierBot.Infrastructure.Models.Api;

/// <summary>
///     Модель расписания
/// </summary>
public sealed class Schedule : IModelWithId
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
    ///     Идентификатор сообщения
    /// </summary>
    [JsonPropertyName("message_id")]
    public long MessageId { get; set; }

    /// <summary>
    ///     Выражение кроны
    /// </summary>
    [JsonPropertyName("cron")]
    public string CronExpression { get; set; } = default!;

    /// <summary>
    ///     Признак активности расписания
    /// </summary>
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}