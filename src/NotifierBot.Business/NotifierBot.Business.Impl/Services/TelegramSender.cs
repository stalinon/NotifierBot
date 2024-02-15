using System.Net;
using NotifierBot.Business.Services;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using NotifierBot.Infrastructure.Models.Abstractions;
using NotifierBot.Infrastructure.Models.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NotifierBot.Business.Impl.Services;

/// <summary>
///     Отправитель для Telegram
/// </summary>
internal sealed class TelegramSender : ISender
{
    /// <inheritdoc />
    public SenderType Type => SenderType.TELEGRAM;
    
    /// <inheritdoc />
    public async Task SendAsync(IMessage message, ISenderRecipientSettings settings, CancellationToken cancellationToken)
    {
        try
        {
            var (client, channelId) = CreateClient(settings);

            if (message.Attachment == null)
            {
                await SendTextAsync(client, channelId, message, cancellationToken);
            }
            else
            {
                await SendAttachmentAsync(client, channelId, message, cancellationToken);
            }
        }
        catch (Exception)
        {
            throw new ErrorException(HttpStatusCode.InternalServerError, "Не удалось отправить сообщение в Телеграмм.");
        }
    }

    private async Task SendTextAsync(
        ITelegramBotClient client,
        long channelId,
        IMessage message,
        CancellationToken cancellationToken)
    {
        try
        {
            _ = await client.SendTextMessageAsync(
                channelId,
                message.Text,
                parseMode: ParseMode.MarkdownV2,
                cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            throw new ErrorException(HttpStatusCode.InternalServerError, "Не удалось отправить сообщение в Телеграмм.");
        }
    }
    
    private async Task SendAttachmentAsync(
        ITelegramBotClient client,
        long channelId,
        IMessage message,
        CancellationToken cancellationToken)
    {
        try
        {
            if (message.Attachment == null)
            {
                throw new Exception("Приложение к сообщению отсутствует.");
            }

            var stream = new MemoryStream(message.Attachment);
            var document = InputFile.FromStream(stream, Guid.NewGuid().ToString("N"));

            _ = message switch
            {
                { AttachmentType: AttachmentType.PHOTO } => await client.SendPhotoAsync(channelId, document, caption: message.Text,
                    cancellationToken: cancellationToken),
                { AttachmentType: AttachmentType.FILE } => await client.SendDocumentAsync(channelId, document, caption: message.Text,
                    cancellationToken: cancellationToken),
                _ => throw new Exception("Приложение к сообщению отсутствует.")
            };
        }
        catch (Exception)
        {
            throw new ErrorException(HttpStatusCode.InternalServerError, "Не удалось отправить сообщение в Телеграмм.");
        }
    }

    private static (ITelegramBotClient Client, long ChannelId) CreateClient(ISenderRecipientSettings settings)
    {
        if (!long.TryParse(settings.ExternalIdentifier, out var channelId))
        {
            throw new ErrorException(HttpStatusCode.BadRequest, "Идентификатор канала Телеграмм неправильный.");
        }

        return (new TelegramBotClient(settings.Token), channelId);
    }
}