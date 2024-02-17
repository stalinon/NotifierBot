using System.Net;
using NotifierBot.Business.Impl.Services.Schedule;
using NotifierBot.Business.Services;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using Quartz;
using IScheduler = NotifierBot.Business.Services.IScheduler;

namespace NotifierBot.Business.Impl.Services;

/// <inheritdoc cref="Business.Services.IScheduler" />
internal sealed class Scheduler : IScheduler
{
    private readonly IDataManagementFacade _dataManagement;
    private readonly ISenderFactory _senders;
    private readonly Quartz.IScheduler _scheduler;

    /// <inheritdoc cref="Scheduler" />
    public Scheduler(IDataManagementFacade dataManagement, ISenderFactory senders)
    {
        _dataManagement = dataManagement;
        _senders = senders;
        _scheduler = SingletonScheduler.Get();
        _scheduler.Start().GetAwaiter().GetResult();
    }
    
    /// <inheritdoc />
    public async Task ScheduleMessageAsync(IScheduleSettings settings, CancellationToken cancellationToken)
    {
        CronExpression.ValidateExpression(settings.CronExpression);

        var message = await _dataManagement.Messages.ReadAsync(
            settings.MessageId,
            cancellationToken);

        if (message == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Не найдено сообщение #{settings.MessageId}");
        }

        var schedule = await _dataManagement.Schedules.ReadAsync(
            settings,
            cancellationToken);

        if (schedule is null or { IsActive: false })
        {
            return;
        }

        var sender = await _dataManagement.Senders.ReadAsync(message.SenderId, cancellationToken);
        var recipient = await _dataManagement.Recipients.ReadAsync(message.RecipientId, cancellationToken);
        var sendSettings = ISenderRecipientSettings.Merge(sender!, recipient!);
        var client = _senders.Get(sender!.Type);

        var job = JobBuilder.Create<RecurringJob>()
            .SetJobData(
                new JobDataMap
                {
                    [RecurringJob.ActionKey] = async () =>
                        await client.SendAsync(message, sendSettings, cancellationToken)
                }
            )
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithIdentity(schedule.Id.ToString())
            .StartNow()
            .WithCronSchedule(schedule.CronExpression)
            .Build();

        await _scheduler.ScheduleJob(job, trigger, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RescheduleMessageAsync(IScheduleSettings settings, string cron, CancellationToken cancellationToken)
    {
        CronExpression.ValidateExpression(cron);
        
        var schedule = await _dataManagement.Schedules.ReadAsync(
            settings,
            cancellationToken);

        if (schedule == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Не найдено расписание #({settings.MessageId}:{settings.CronExpression})");
        }

        if (!schedule.IsActive)
        {
            await UnscheduleMessageAsync(settings, cancellationToken);
            return;
        }

        var oldTrigger = await _scheduler.GetTrigger(new TriggerKey(schedule.Id.ToString()), cancellationToken);
        switch (oldTrigger)
        {
            case null when !schedule.IsActive:
                return;
            case null when schedule.IsActive:
                await ScheduleMessageAsync(schedule, cancellationToken);
                return;
        }

        var newTrigger = TriggerBuilder.Create()
            .WithIdentity(schedule.Id.ToString())
            .StartNow()
            .WithCronSchedule(cron)
            .Build();

        schedule.CronExpression = cron;
        await _dataManagement.Schedules.UpdateAsync(schedule, cancellationToken);

        await _scheduler.RescheduleJob(oldTrigger!.Key, newTrigger, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UnscheduleMessageAsync(IScheduleSettings settings, CancellationToken cancellationToken)
    {
        var schedule = await _dataManagement.Schedules.ReadAsync(
            settings,
            cancellationToken);

        if (schedule == null)
        {
            throw new ErrorException(HttpStatusCode.NotFound, $"Не найдено расписание #({settings.MessageId}:{settings.CronExpression})");
        }

        var trigger = await _scheduler.GetTrigger(new TriggerKey(schedule.Id.ToString()), cancellationToken);
        if (trigger == null)
        {
            return;
        }
        
        await _scheduler.UnscheduleJob(trigger.Key, cancellationToken);
    }
}