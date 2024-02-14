using Microsoft.EntityFrameworkCore;
using NotifierBot.Data.Entities;

namespace NotifierBot.Data.Impl;

/// <summary>
///     Контекст БД
/// </summary>
internal sealed class DatabaseContext : DbContext
{
    /// <summary>
    ///     Отправители
    /// </summary>
    public DbSet<SenderEntity> Senders { get; set; } = default!;

    /// <summary>
    ///     Получатели
    /// </summary>
    public DbSet<RecipientEntity> Recipients { get; set; } = default!;

    /// <summary>
    ///     Сообщения
    /// </summary>
    public DbSet<MessageEntity> Messages { get; set; } = default!;

    /// <summary>
    ///     Расписания
    /// </summary>
    public DbSet<ScheduleEntity> Schedules { get; set; } = default!;
    
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SenderEntity.Setup(modelBuilder);
        RecipientEntity.Setup(modelBuilder);
        MessageEntity.Setup(modelBuilder);
        ScheduleEntity.Setup(modelBuilder);
    }
}