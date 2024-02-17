using System.Net;
using NotifierBot.Business.Impl.Mapping;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data;
using NotifierBot.Data.Entities;
using NotifierBot.Data.Repositories;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using Quartz;
using IScheduler = NotifierBot.Business.Services.IScheduler;

namespace NotifierBot.Business.Impl.Services.DataManagement;

/// <inheritdoc cref="IScheduleManager" />
internal sealed class ScheduleManager : DataManager<ScheduleEntity, Infrastructure.Models.Api.Schedule>, IScheduleManager
{
    private readonly IDataAccessFacade _database;
    private readonly IScheduler _scheduler;

    /// <inheritdoc cref="ScheduleManager" />
    public ScheduleManager(IDataAccessFacade database, IScheduler scheduler) : base(new ScheduleMapper(), database)
    {
        _database = database;
        _scheduler = scheduler;
    }

    /// <inheritdoc />
    protected override Func<IDataAccessFacade, IRepository<ScheduleEntity>> RepositorySelector => d => d.Schedules;

    /// <inheritdoc />
    public async Task<Infrastructure.Models.Api.Schedule?> ReadAsync(IScheduleSettings scheduleSettings, CancellationToken cancellationToken)
    {
        var entity = await GetScheduleBySettingsAsync(scheduleSettings, cancellationToken);

        return await base.ReadAsync(entity.Id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Infrastructure.Models.Api.Schedule> DeleteAsync(IScheduleSettings scheduleSettings, CancellationToken cancellationToken)
    {
        var entity = await GetScheduleBySettingsAsync(scheduleSettings, cancellationToken);
        await _scheduler.UnscheduleMessageAsync(scheduleSettings, cancellationToken);

        return await base.DeleteAsync(entity.Id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<Infrastructure.Models.Api.Schedule> CreateAsync(Infrastructure.Models.Api.Schedule model, CancellationToken cancellationToken)
    {
        await ValidateScheduleAsync(model, cancellationToken);

        var schedule = await base.CreateAsync(model, cancellationToken);
        await _scheduler.ScheduleMessageAsync(schedule, cancellationToken);
        
        return schedule;
    }

    /// <inheritdoc />
    public override async Task<Infrastructure.Models.Api.Schedule> UpdateAsync(Infrastructure.Models.Api.Schedule model, CancellationToken cancellationToken)
    {
        await ValidateScheduleAsync(model, cancellationToken);

        var schedule = await base.CreateAsync(model, cancellationToken);
        await _scheduler.RescheduleMessageAsync(schedule, schedule.CronExpression, cancellationToken);
        
        return schedule;
    }

    private async Task ValidateScheduleAsync(Infrastructure.Models.Api.Schedule model, CancellationToken cancellationToken)
    {
        var message = await _database.Messages.FirstOrDefaultAsync(e => e.Id == model.MessageId, cancellationToken);
        if (message == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Не найдено сообщение #{model.MessageId}");
        }

        if (!CronExpression.IsValidExpression(model.CronExpression))
        {
            throw new ErrorException(HttpStatusCode.BadRequest, $"Выражение неверно '{model.CronExpression}'");
        }
    }

    private async Task<ScheduleEntity> GetScheduleBySettingsAsync(IScheduleSettings scheduleSettings, CancellationToken cancellationToken)
    {
        var entity = await RepositorySelector(_database).FirstOrDefaultAsync(
            e => e.MessageId == scheduleSettings.MessageId && e.CronExpression == scheduleSettings.CronExpression,
            cancellationToken);

        if (entity == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound,
                $"Сущность ({scheduleSettings.MessageId}:{scheduleSettings.CronExpression}) не была найдена.");
        }

        return entity;
    }
}