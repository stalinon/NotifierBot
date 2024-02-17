using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Business.Services.DataManagement;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="IDataManagementFacade" />
internal sealed class DataManagementFacade : IDataManagementFacade
{
    private readonly IServiceProvider _serviceProvider;

    /// <inheritdoc cref="DataManagementFacade" />
    public DataManagementFacade(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    /// <inheritdoc />
    public ISenderManager Senders
        => _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISenderManager>();
    
    /// <inheritdoc />
    public IRecipientManager Recipients
        => _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRecipientManager>();
    
    /// <inheritdoc />
    public IMessageManager Messages
        => _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMessageManager>();
    
    /// <inheritdoc />
    public IScheduleManager Schedules
        => _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IScheduleManager>();
}