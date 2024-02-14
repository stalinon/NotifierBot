namespace NotifierBot.Configuration.Impl;

/// <inheritdoc cref="IBotSettings" />
internal class BotSettings : IBotSettings
{
    /// <inheritdoc />
    public string Key { get; init; } = string.Empty;
}