using NotifierBot.Configuration;
using NotifierBot.Configuration.Impl;

namespace NotifierBot;

/// <summary>
///     Класс создания настроек для отправки сообщений
/// </summary>
public static class SettingsCreator
{
    /// <summary>
    ///     Создать настройки для отправки сообщения в Telegram
    /// </summary>
    public static ITargetSettings TelegramSettings(string botKey, string chatId)
    {
        return new TargetSettings
        {
            Key = botKey, TargetId = chatId
        };
    }
}