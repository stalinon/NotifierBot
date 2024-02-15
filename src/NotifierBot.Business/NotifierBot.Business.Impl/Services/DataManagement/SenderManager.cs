using NotifierBot.Business.Impl.Mapping;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="ISenderManager" />
internal sealed class SenderManager : DataManager<SenderEntity, Sender>, ISenderManager
{
    private readonly IDataAccessFacade _database;

    /// <inheritdoc cref="SenderManager" />
    public SenderManager(IDataAccessFacade database) : base(new SenderMapper(), database)
    {
        _database = database;
    }

    /// <inheritdoc />
    protected override Func<IDataAccessFacade, IRepository<SenderEntity>> RepositorySelector => d => d.Senders;

    /// <inheritdoc />
    public override async Task<Sender> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var schedules = await _database.Messages.GetAllAsync(s => s.SenderId == id, cancellationToken: cancellationToken);
        _database.Messages.RemoveRange(schedules);
        await _database.Messages.SaveChangesAsync(cancellationToken);
        
        return await base.DeleteAsync(id, cancellationToken);
    }
}