using NotifierBot.Configuration;
using NotifierBot.Models;

namespace NotifierBot.Services;
    
/// <summary>
/// Менеджер уведомлений
/// </summary>
public interface INotifyManager
{
    /// <summary>
    ///     Отправляет сообщение
    /// </summary>
    /// <param name="settings">Настройки уведомлений</param>
    /// <param name="message">Сообщения</param>
    Task SendMessageAsync(ITargetSettings settings, NotifyMessage message);

    /// <summary>
    ///     Отправляет вложение
    /// </summary>
    /// <param name="settings">Настройки уведомлений</param>
    /// <param name="attachment">Вложение</param>
    Task SendAttachmentAsync(ITargetSettings settings, NotifyAttachment attachment);
}