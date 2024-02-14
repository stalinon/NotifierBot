using System.Net;
using Microsoft.EntityFrameworkCore;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Maintenance.Exceptions;

namespace NotifierBot.Data.Impl.Repositories;

/// <inheritdoc cref="IMessageRepository" />
internal sealed class MessageRepository : Repository<MessageEntity>, IMessageRepository
{
    /// <inheritdoc cref="MessageRepository" />
    public MessageRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public override Task AddAsync(MessageEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity.Sender.Type.ToString() != entity.Recipient.ToString())
        {
            throw new ErrorException(HttpStatusCode.Conflict, "Типы отправителя и получателя не совпадают.");
        }
        
        return base.AddAsync(entity, cancellationToken);
    }

    public override Task AddRangeAsync(IEnumerable<MessageEntity> entities, CancellationToken cancellationToken = default)
    {
        var messageEntities = entities as MessageEntity[] ?? entities.ToArray();
        foreach (var entity in messageEntities)
        {
            if (entity.Sender.Type.ToString() != entity.Recipient.ToString())
            {
                throw new ErrorException(HttpStatusCode.Conflict, "Типы отправителя и получателя не совпадают.");
            }
        }
        
        return base.AddRangeAsync(messageEntities, cancellationToken);
    }
}