using Microsoft.EntityFrameworkCore;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories;

/// <inheritdoc cref="ISenderRepository" />
internal sealed class SenderRepository : Repository<SenderEntity>, ISenderRepository
{
    /// <inheritdoc cref="SenderRepository" />
    public SenderRepository(DbContext dbContext) : base(dbContext)
    {
    }
}