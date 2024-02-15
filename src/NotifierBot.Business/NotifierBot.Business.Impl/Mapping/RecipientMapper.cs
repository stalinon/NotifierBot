using NotifierBot.Data.Entities;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Mapping;

/// <summary>
///     Маппер получателей
/// </summary>
internal sealed class RecipientMapper : Mapper<RecipientEntity, Recipient>
{
    /// <inheritdoc />
    public override Recipient Map(RecipientEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Created = entity.Created,
            Updated = entity.Updated,
            Name = entity.Name,
            Type = entity.Type,
            ExternalIdentifier = entity.ExternalIdentifier
        };
    }

    /// <inheritdoc />
    public override RecipientEntity Map(Recipient model)
    {
        return new()
        {
            Id = model.Id,
            Created = model.Created,
            Updated = model.Updated,
            Name = model.Name,
            Type = model.Type,
            ExternalIdentifier = model.ExternalIdentifier
        };
    }

    /// <inheritdoc />
    public override Recipient Map(RecipientEntity entity, Recipient model)
    {
        model.Id = entity.Id;
        model.Created = entity.Created;
        model.Updated = entity.Updated;
        model.Name = entity.Name;
        model.Type = entity.Type;
        model.ExternalIdentifier = entity.ExternalIdentifier;

        return model;
    }

    /// <inheritdoc />
    public override RecipientEntity Map(Recipient model, RecipientEntity entity)
    {
        entity.Id = model.Id;
        entity.Created = model.Created;
        entity.Updated = model.Updated;
        entity.Name = model.Name;
        entity.Type = model.Type;
        entity.ExternalIdentifier = model.ExternalIdentifier;

        return entity;
    }
}