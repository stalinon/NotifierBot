using Quartz;

namespace NotifierBot.Services;

/// <summary>
///     Мастер расписаний
/// </summary>
public interface IScheduler
{
    /// <summary>
    ///     Добавить задачу в расписание
    /// </summary>
    Task<TriggerKey> ScheduleAsync(string cronExpression, string code);

    /// <summary>
    ///     Перезапустить задачу заново
    /// </summary>
    Task RescheduleAsync(string cronExpression, string triggerName);

    /// <summary>
    ///     Убрать задачу из расписания
    /// </summary>
    Task UnScheduleAsync(TriggerKey triggerKey);

    /// <summary>
    ///     Убрать неколько задач из расписания
    /// </summary>
    Task UnScheduleAsync(List<TriggerKey> triggerKeys);
}