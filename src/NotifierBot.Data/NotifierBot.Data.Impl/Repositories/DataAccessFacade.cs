using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories;

/// <inheritdoc cref="IDataAccessFacade" />
internal sealed class DataAccessFacade : IDataAccessFacade
{
    /// <inheritdoc cref="DataAccessFacade" />
    public DataAccessFacade(
        IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        
        Schedules = scope.ServiceProvider.GetRequiredService<IScheduleRepository>();
        Messages = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
        Senders = scope.ServiceProvider.GetRequiredService<ISenderRepository>();
        Recipients = scope.ServiceProvider.GetRequiredService<IRecipientRepository>();
    }
    
    /// <inheritdoc />
    public IScheduleRepository Schedules { get; }
    
    /// <inheritdoc />
    public IMessageRepository Messages { get; }
    
    /// <inheritdoc />
    public ISenderRepository Senders { get; }
    
    /// <inheritdoc />
    public IRecipientRepository Recipients { get; }
}