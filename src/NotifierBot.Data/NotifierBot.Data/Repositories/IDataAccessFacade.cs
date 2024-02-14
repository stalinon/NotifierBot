namespace NotifierBot.Data.Repositories;

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