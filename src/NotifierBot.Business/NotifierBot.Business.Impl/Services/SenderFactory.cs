using NotifierBot.Business.Services;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Business.Impl.Services;

/// <inheritdoc cref="ISenderFactory" />
internal sealed class SenderFactory : ISenderFactory
{
    /// <inheritdoc cref="SenderFactory" />
    public SenderFactory()
    {
    }

    /// <inheritdoc />
    public ISender Get(SenderType type)
    {
        return type switch
        {
            SenderType.TELEGRAM => new TelegramSender(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}