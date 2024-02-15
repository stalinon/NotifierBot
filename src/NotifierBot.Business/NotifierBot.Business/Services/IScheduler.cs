using NotifierBot.Infrastructure.Maintenance.Configuration;

namespace NotifierBot.Business.Services;

/// <summary>
///     Мастер расписаний
/// </summary>
public interface IScheduler
{
    /// <summary>
    ///     Установить сообщение в расписание
    /// </summary>
    Task ScheduleMessageAsync(IScheduleSettings settings, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Установить сообщение в новое расписание расписание
    /// </summary>
    Task RescheduleMessageAsync(IScheduleSettings settings, string cron, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Убрать сообщение из расписания
    /// </summary>
    Task UnscheduleMessageAsync(IScheduleSettings settings, CancellationToken cancellationToken);
}