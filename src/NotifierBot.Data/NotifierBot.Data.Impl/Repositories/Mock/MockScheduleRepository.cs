using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Мок <see cref="IScheduleRepository" />
/// </summary>
internal sealed class MockScheduleRepository : MockRepository<ScheduleEntity>, IScheduleRepository
{
    /// <inheritdoc cref="MockScheduleRepository" />
    public MockScheduleRepository() : base(EnvironmentDataGenerator.Schedules)
    { }
}