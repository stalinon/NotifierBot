using NotifierBot.Configuration;
using NotifierBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NotifierBot.Services.Impl;

/// <summary>
///     Реализация <see cref="INotifyManager" /> для телеграмма
/// </summary>
internal sealed class TelegramNotifyManager : INotifyManager
{
    #region Implementation INotifyManager

    /// <inheritdoc />
    public async Task SendMessageAsync(ITargetSettings settings, NotifyMessage message)
    {
        if (message == null)
        {
            throw new Exception("Notify message is null");
        }

        var (client, channelId) = CreateClient(settings);

        var text = $"*{message.Title}*\n{message.Text}";

        _ = await client.SendTextMessageAsync(
            channelId,
            text,
            parseMode: ParseMode.MarkdownV2);
    }

    /// <inheritdoc/>
    public async Task SendAttachmentAsync(ITargetSettings settings, NotifyAttachment attachment)
    {
        if (attachment == null)
        {
            throw new Exception("Notify attachment is null");
        }

        var (client, channelId) = CreateClient(settings);

        var document = InputFile.FromStream(attachment.Stream, attachment.Filename);
        _ = await client.SendDocumentAsync(channelId, document, caption: attachment.Caption);
    }

    #endregion

    #region Private methods

    private static (TelegramBotClient Client, long ChannelId) CreateClient(ITargetSettings settings)
    {
        if (!long.TryParse(settings.TargetId, out var channelId))
        {
            throw new Exception("Telegram Channel incorrect");
        }

        return (new TelegramBotClient(settings.Key), channelId);
    }

    #endregion
}