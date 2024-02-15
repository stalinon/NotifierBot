using NotifierBot.Data.Entities;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Mapping;

/// <summary>
///     Маппер отправителей
/// </summary>
internal sealed class ScheduleMapper : Mapper<ScheduleEntity, Schedule>
{
    /// <inheritdoc />
    public override Schedule Map(ScheduleEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Created = entity.Created,
            Updated = entity.Updated,
            MessageId = entity.MessageId,
            CronExpression = entity.CronExpression,
            IsActive = entity.IsActive
        };
    }

    /// <inheritdoc />
    public override ScheduleEntity Map(Schedule model)
    {
        return new()
        {
            Id = model.Id,
            Created = model.Created,
            Updated = model.Updated,
            MessageId = model.MessageId,
            CronExpression = model.CronExpression,
            IsActive = model.IsActive
        };
    }

    /// <inheritdoc />
    public override Schedule Map(ScheduleEntity entity, Schedule model)
    {
        model.Id = entity.Id;
        model.Created = entity.Created;
        model.Updated = entity.Updated;
        model.MessageId = entity.MessageId;
        model.CronExpression = entity.CronExpression;
        model.IsActive = entity.IsActive;

        return model;
    }

    /// <inheritdoc />
    public override ScheduleEntity Map(Schedule model, ScheduleEntity entity)
    {
        entity.Id = model.Id;
        entity.Created = model.Created;
        entity.Updated = model.Updated;
        entity.MessageId = model.MessageId;
        entity.CronExpression = model.CronExpression;
        entity.IsActive = model.IsActive;

        return entity;
    }
}