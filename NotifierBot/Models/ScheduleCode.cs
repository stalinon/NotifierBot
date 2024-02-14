namespace NotifierBot.Models;

/// <summary>
///     Код расписания
/// </summary>
public sealed record ScheduleCode(string Code, Cron Cron);