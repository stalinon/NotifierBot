using NotifierBot.Data.Entities;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Mapping;

/// <summary>
///     Маппер отправителей
/// </summary>
internal sealed class SenderMapper : Mapper<SenderEntity, Sender>
{
    /// <inheritdoc />
    public override Sender Map(SenderEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Created = entity.Created,
            Updated = entity.Updated,
            Name = entity.Name,
            Token = entity.Token,
            Type = entity.Type
        };
    }

    /// <inheritdoc />
    public override SenderEntity Map(Sender model)
    {
        return new()
        {
            Id = model.Id,
            Created = model.Created,
            Updated = model.Updated,
            Name = model.Name,
            Token = model.Token,
            Type = model.Type
        };
    }

    /// <inheritdoc />
    public override Sender Map(SenderEntity entity, Sender model)
    {
        model.Id = entity.Id;
        model.Created = entity.Created;
        model.Updated = entity.Updated;
        model.Name = entity.Name;
        model.Type = entity.Type;

        return model;
    }

    /// <inheritdoc />
    public override SenderEntity Map(Sender model, SenderEntity entity)
    {
        entity.Id = model.Id;
        entity.Created = model.Created;
        entity.Updated = model.Updated;
        entity.Name = model.Name;
        entity.Type = model.Type;

        return entity;
    }
}