namespace NotifierBot.Infrastructure.Maintenance.Configuration;

/// <summary>
///     Настройки задачи по расписанию
/// </summary>
public interface IScheduleSettings
{
    /// <summary>
    ///     Идентификатор сообщения
    /// </summary>
    long MessageId { get; }

    /// <summary>
    ///     Выражение для установки на расписание
    /// </summary>
    string CronExpression { get; }
}