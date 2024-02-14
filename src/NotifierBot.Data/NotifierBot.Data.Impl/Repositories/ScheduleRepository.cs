using Microsoft.EntityFrameworkCore;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;

namespace NotifierBot.Data.Impl.Repositories;

/// <inheritdoc cref="IScheduleRepository" />
internal sealed class ScheduleRepository : Repository<ScheduleEntity>, IScheduleRepository
{
    /// <inheritdoc cref="ScheduleRepository" />
    public ScheduleRepository(DbContext dbContext) : base(dbContext)
    {
    }
}