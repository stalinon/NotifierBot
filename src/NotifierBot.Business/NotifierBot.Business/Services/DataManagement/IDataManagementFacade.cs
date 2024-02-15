namespace NotifierBot.Business.Services.DataManagement;

/// <summary>
///     Фасад управления данными
/// </summary>
public interface IDataManagementFacade
{
    /// <summary>
    ///     Отправители
    /// </summary>
    ISenderManager Senders { get; }
    
    /// <summary>
    ///     Получатели
    /// </summary>
    IRecipientManager Recipients { get; }
    
    /// <summary>
    ///     Сообщения
    /// </summary>
    IMessageManager Messages { get; }
    
    /// <summary>
    ///     Расписания
    /// </summary>
    IScheduleManager Schedules { get; }
}