using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Мок <see cref="IMessageRepository" />
/// </summary>
internal sealed class MockMessageRepository : MockRepository<MessageEntity>, IMessageRepository
{
    /// <inheritdoc cref="MockMessageRepository" />
    public MockMessageRepository() : base(EnvironmentDataGenerator.Messages)
    { }
}