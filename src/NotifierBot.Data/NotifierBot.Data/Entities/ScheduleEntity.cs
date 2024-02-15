using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NotifierBot.Infrastructure.Maintenance.Configuration;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Расписания
/// </summary>
[Table("schedules")]
public sealed class ScheduleEntity : IHasId, IEntityLifecycle, IConfigurable, IScheduleSettings
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

    /// <inheritdoc />
    [Column("message_id"), ForeignKey(nameof(Message))]
    public long MessageId { get; set; }

    /// <inheritdoc />
    [Column("cron")]
    public string CronExpression { get; set; } = default!;

    /// <summary>
    ///     Сообщение
    /// </summary>
    public MessageEntity Message { get; set; } = default!;

    /// <summary>
    ///     Признак активности расписания
    /// </summary>
    [Column("is_active")]
    public bool IsActive { get; set; }

    /// <inheritdoc />
    public static void Setup(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<ScheduleEntity>();
        entityBuilder.HasIndex(p => new { p.MessageId, p.CronExpression }).IsUnique();
        entityBuilder.HasIndex(p => p.IsActive);
    }
}