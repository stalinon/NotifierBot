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
    public MessageRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}