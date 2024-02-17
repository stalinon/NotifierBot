using NotifierBot.Data.Entities;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Impl.Repositories.Mock;

/// <summary>
///     Генератор тестовых данных для окружения
/// </summary>
internal static class EnvironmentDataGenerator
{
    private const long Limit = 100;
    
    private static readonly Random Random = new();
    private static Data? _bag;

    public static List<SenderEntity> Senders => _bag!.Senders;
    public static List<RecipientEntity> Recipients => _bag!.Recipients;
    public static List<MessageEntity> Messages => _bag!.Messages;
    public static List<ScheduleEntity> Schedules => _bag!.Schedules;
    
    public static void GenerateIfEmpty()
    {
        if (_bag != null)
        {
            return;
        }
        
        var senders = new List<SenderEntity>();
        for (var i = 1; i < Limit; i++)
        {
            var sender = new SenderEntity
            {
                Id = i,
                Name = Guid.NewGuid().ToString("N"),
                Token = Guid.NewGuid().ToString("N"),
                Type = SenderType.TELEGRAM,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            
            senders.Add(sender);
        }
        
        var recipients = new List<RecipientEntity>();
        for (var i = 1; i < Limit; i++)
        {
            var recipient = new RecipientEntity
            {
                Id = i,
                Name = Guid.NewGuid().ToString("N"),
                ExternalIdentifier = Guid.NewGuid().ToString("N"),
                Type = RecipientType.TELEGRAM,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            
            recipients.Add(recipient);
        }
        
        var messages = new List<MessageEntity>();
        for (var i = 1; i < Limit; i++)
        {
            var randomSender = senders.GetRandomElement();
            var randomRecipient = recipients.GetRandomElement();
            
            var message = new MessageEntity
            {
                Id = i,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Content = new MessageContent
                {
                    Text = Guid.NewGuid().ToString("N")
                },
                RecipientId = randomRecipient.Id,
                Recipient = randomRecipient,
                SenderId = randomSender.Id,
                Sender = randomSender
            };
            
            messages.Add(message);
        }
        
        var schedules = new List<ScheduleEntity>();
        for (var i = 1; i < Limit; i++)
        {
            var randomMessage = messages.GetRandomElement();
            
            var schedule = new ScheduleEntity
            {
                Id = i,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                CronExpression = "0 0 0/1 1/1 * ? *",
                MessageId = randomMessage.Id,
                Message = randomMessage,
                IsActive = false
            };
            
            schedules.Add(schedule);
        }

        _bag = new(senders, recipients, messages, schedules);
    }

    private static T GetRandomElement<T>(this IList<T> collection)
    {
        var index = Random.Next(collection.Count);
        return collection[index];
    }

    private sealed record Data(
        List<SenderEntity> Senders,
        List<RecipientEntity> Recipients,
        List<MessageEntity> Messages,
        List<ScheduleEntity> Schedules);
}