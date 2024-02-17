using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl;

/// <inheritdoc cref="IDataAccessFacade" />
internal sealed class DataAccessFacade : IDataAccessFacade
{
    private readonly IServiceProvider _serviceProvider;

    /// <inheritdoc cref="DataAccessFacade" />
    public DataAccessFacade(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public IScheduleRepository Schedules =>
        _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IScheduleRepository>();
    
    /// <inheritdoc />
    public IMessageRepository Messages =>
        _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMessageRepository>();
    
    /// <inheritdoc />
    public ISenderRepository Senders =>
        _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISenderRepository>();
    
    /// <inheritdoc />
    public IRecipientRepository Recipients =>
        _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRecipientRepository>();
}