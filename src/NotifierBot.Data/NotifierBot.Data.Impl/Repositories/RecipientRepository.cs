using Microsoft.EntityFrameworkCore;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories;

/// <inheritdoc cref="IRecipientRepository" />
internal sealed class RecipientRepository : Repository<RecipientEntity>, IRecipientRepository
{
    /// <inheritdoc cref="RecipientRepository" />
    public RecipientRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}