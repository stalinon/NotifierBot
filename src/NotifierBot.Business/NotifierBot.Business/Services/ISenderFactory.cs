using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Business.Services;

/// <summary>
///     Фабрика отправителей
/// </summary>
public interface ISenderFactory
{
    /// <summary>
    ///     Получить отправителя по типу
    /// </summary>
    ISender Get(SenderType type);
}