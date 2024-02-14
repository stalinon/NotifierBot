namespace NotifierBot.Configuration.Impl;

/// <inheritdoc cref="ITargetSettings" />
internal sealed class TargetSettings : BotSettings, ITargetSettings
{
    /// <inheritdoc />
    public string TargetId { get; init; } = string.Empty;
}