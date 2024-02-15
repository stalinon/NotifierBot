using NotifierBot.Data.Repositories;

namespace NotifierBot.Data;

/// <summary>
///     Единая точка доступа в БД
/// </summary>
public interface IDataAccessFacade
{
    /// <inheritdoc cref="IScheduleRepository" />
    IScheduleRepository Schedules { get; }
    
    /// <inheritdoc cref="IMessageRepository" />
    IMessageRepository Messages { get; }
    
    /// <inheritdoc cref="ISenderRepository" />
    ISenderRepository Senders { get; }
    
    /// <inheritdoc cref="IRecipientRepository" />
    IRecipientRepository Recipients { get; }
}