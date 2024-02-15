using System.Net;
using NotifierBot.Business.Impl.Mapping;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="IMessageManager" />
internal sealed class MessageManager : DataManager<MessageEntity, Message>, IMessageManager
{
    private readonly IDataAccessFacade _database;

    /// <inheritdoc cref="MessageManager" />
    public MessageManager(IDataAccessFacade database) : base(new MessageMapper(), database)
    {
        _database = database;
    }

    /// <inheritdoc />
    protected override Func<IDataAccessFacade, IRepository<MessageEntity>> RepositorySelector => d => d.Messages;

    /// <inheritdoc />
    public override async Task<Message> CreateAsync(Message model, CancellationToken cancellationToken)
    {
        await ValidateMessage(model, cancellationToken);
        
        return await base.CreateAsync(model, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<Message> UpdateAsync(Message model, CancellationToken cancellationToken)
    {
        await ValidateMessage(model, cancellationToken);

        return await base.UpdateAsync(model, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<Message> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var schedules = await _database.Schedules.GetAllAsync(s => s.MessageId == id, cancellationToken: cancellationToken);
        _database.Schedules.RemoveRange(schedules);
        await _database.Schedules.SaveChangesAsync(cancellationToken);
        
        return await base.DeleteAsync(id, cancellationToken);
    }

    private async Task ValidateMessage(Message model, CancellationToken cancellationToken)
    {
        var sender = await _database.Senders.FirstOrDefaultAsync(e => e.Id == model.SenderId, cancellationToken);
        if (sender == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Не найден отправитель #{model.SenderId}");
        }

        var recipient = await _database.Recipients.FirstOrDefaultAsync(e => e.Id == model.RecipientId, cancellationToken);
        if (recipient == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Не найден получатель #{model.RecipientId}");
        }

        if (recipient.Type.ToString() != sender.Type.ToString())
        {
            throw new ErrorException(
                HttpStatusCode.BadRequest,
                $"Отправитель #{model.SenderId} и получатель #{model.RecipientId} расположены на разных платформах.");
        }
    }
}