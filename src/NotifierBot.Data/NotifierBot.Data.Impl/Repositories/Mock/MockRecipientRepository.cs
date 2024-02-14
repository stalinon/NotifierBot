using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Мок <see cref="ISenderRepository" />
/// </summary>
internal sealed class MockRecipientRepository : MockRepository<RecipientEntity>, IRecipientRepository
{
    /// <inheritdoc cref="MockRecipientRepository" />
    public MockRecipientRepository() : base(EnvironmentDataGenerator.Recipients)
    { }
}