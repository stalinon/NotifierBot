using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Models.Abstractions;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Business.Services;

/// <summary>
///     Сервис отправки сообщений
/// </summary>
public interface ISender
{
    /// <summary>
    ///     Тип отправителя
    /// </summary>
    SenderType Type { get; }
    
    /// <summary>
    ///     Отправить сообщение
    /// </summary>
    Task SendAsync(IMessage message, ISenderRecipientSettings settings, CancellationToken cancellationToken);
}