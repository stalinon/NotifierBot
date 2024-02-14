using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Мок <see cref="ISenderRepository" />
/// </summary>
internal sealed class MockSenderRepository : MockRepository<SenderEntity>, ISenderRepository
{
    /// <inheritdoc cref="MockSenderRepository" />
    public MockSenderRepository() : base(EnvironmentDataGenerator.Senders)
    { }
}