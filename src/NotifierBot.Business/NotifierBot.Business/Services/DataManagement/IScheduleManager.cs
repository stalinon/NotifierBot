using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Services.DataManagement;

/// <summary>
///     Сервис управления данными расписаний
/// </summary>
public interface IScheduleManager : IDataManager<Schedule>
{
    /// <summary>
    ///     Читать
    /// </summary>
    Task<Schedule?> ReadAsync(
        IScheduleSettings scheduleSettings,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить
    /// </summary>
    Task<Schedule> DeleteAsync(
        IScheduleSettings scheduleSettings,
        CancellationToken cancellationToken);
}