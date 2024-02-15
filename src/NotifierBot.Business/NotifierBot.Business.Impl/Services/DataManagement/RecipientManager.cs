using NotifierBot.Business.Impl.Mapping;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="IRecipientManager" />
internal sealed class RecipientManager : DataManager<RecipientEntity, Recipient>, IRecipientManager
{
    private readonly IDataAccessFacade _database;

    /// <inheritdoc cref="RecipientManager" />
    public RecipientManager(IDataAccessFacade database) : base(new RecipientMapper(), database)
    {
        _database = database;
    }

    /// <inheritdoc />
    protected override Func<IDataAccessFacade, IRepository<RecipientEntity>> RepositorySelector => d => d.Recipients;

    /// <inheritdoc />
    public override async Task<Recipient> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var schedules = await _database.Messages.GetAllAsync(s => s.RecipientId == id, cancellationToken: cancellationToken);
        _database.Messages.RemoveRange(schedules);
        await _database.Messages.SaveChangesAsync(cancellationToken);
        
        return await base.DeleteAsync(id, cancellationToken);
    }
}