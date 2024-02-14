using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Расписания
/// </summary>
[Table("schedules")]
public sealed class ScheduleEntity : IHasId, IEntityLifecycle, IConfigurable
{
    /// <inheritdoc />
    [Column("id")]
    public long Id { get; set; }
    
    /// <inheritdoc />
    [Column("created")]
    public DateTime Created { get; set; }

    /// <inheritdoc />
    [Column("updated")]
    public DateTime Updated { get; set; }

    /// <summary>
    ///     Идентификатор сообщения
    /// </summary>
    [Column("message_id"), ForeignKey(nameof(Message))]
    public long MessageId { get; set; }

    /// <summary>
    ///     Сообщение
    /// </summary>
    public MessageEntity Message { get; set; } = default!;

    /// <summary>
    ///     Выражение для установки на расписание
    /// </summary>
    [Column("cron")]
    public string CronExpression { get; set; } = default!;

    /// <summary>
    ///     Признак активности расписания
    /// </summary>
    [Column("is_active")]
    public bool IsActive { get; set; }

    /// <inheritdoc />
    public static void Setup(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<ScheduleEntity>();
        entityBuilder.HasIndex(p => p.MessageId).IsUnique();
        entityBuilder.HasIndex(p => p.IsActive);
    }
}