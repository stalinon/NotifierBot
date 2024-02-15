using NotifierBot.Data.Entities;
using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Impl.Mapping;

/// <summary>
///     Маппер сообщений
/// </summary>
internal sealed class MessageMapper : Mapper<MessageEntity, Message>
{
    /// <inheritdoc />
    public override Message Map(MessageEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Created = entity.Created,
            Updated = entity.Updated,
            Text = entity.Content.Text,
            Attachment = entity.Content.Attachment,
            AttachmentType = entity.Content.AttachmentType,
            SenderId = entity.SenderId,
            RecipientId = entity.RecipientId
        };
    }

    /// <inheritdoc />
    public override MessageEntity Map(Message model)
    {
        return new()
        {
            Id = model.Id,
            Created = model.Created,
            Updated = model.Updated,
            SenderId = model.SenderId,
            RecipientId = model.RecipientId,
            Content = new()
            {
                Text = model.Text,
                Attachment = model.Attachment,
                AttachmentType = model.AttachmentType
            }
        };
    }

    /// <inheritdoc />
    public override Message Map(MessageEntity entity, Message model)
    {
        model.Id = entity.Id;
        model.Created = entity.Created;
        model.Updated = entity.Updated;
        model.Text = entity.Content.Text;
        model.Attachment = entity.Content.Attachment;
        model.AttachmentType = entity.Content.AttachmentType;
        model.SenderId = entity.SenderId;
        model.RecipientId = entity.RecipientId;
        
        return model;
    }

    /// <inheritdoc />
    public override MessageEntity Map(Message model, MessageEntity entity)
    {
        entity.Id = model.Id;
        entity.Created = model.Created;
        entity.Updated = model.Updated;
        entity.SenderId = model.SenderId;
        entity.RecipientId = model.RecipientId;
        entity.Content.Text = model.Text;
        entity.Content.Attachment = model.Attachment;
        entity.Content.AttachmentType = model.AttachmentType;

        return entity;
    }
}