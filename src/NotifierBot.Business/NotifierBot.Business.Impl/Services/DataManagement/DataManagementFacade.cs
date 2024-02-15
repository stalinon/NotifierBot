using NotifierBot.Business.Services.DataManagement;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="IDataManagementFacade" />
internal sealed class DataManagementFacade : IDataManagementFacade
{
    /// <inheritdoc cref="DataManagementFacade" />
    public DataManagementFacade(
        ISenderManager senders,
        IRecipientManager recipients,
        IMessageManager messages,
        IScheduleManager schedules)
    {
        Senders = senders;
        Recipients = recipients;
        Messages = messages;
        Schedules = schedules;
    }
    
    /// <inheritdoc />
    public ISenderManager Senders { get; }
    
    /// <inheritdoc />
    public IRecipientManager Recipients { get; }
    
    /// <inheritdoc />
    public IMessageManager Messages { get; }
    
    /// <inheritdoc />
    public IScheduleManager Schedules { get; }
}