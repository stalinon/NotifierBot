namespace NotifierBot.Models;

/// <summary>
///     Сообщение-вложение
/// </summary>
public class NotifyAttachment
{
    #region Properties

    /// <summary>
    ///     Описание вложения
    /// </summary>
    public string? Caption { get; internal set; }

    /// <summary>
    ///     Описание вложения
    /// </summary>
    public string? Filename { get; internal set; }

    /// <summary>
    ///     Поток с файлом
    /// </summary>
    public Stream Stream { get; }

    #endregion

    #region .ctor

    /// <inheritdoc cref="NotifyAttachment" />
    public NotifyAttachment(Stream stream, string? filename = null, string? caption = null)
    {
        Caption = caption;
        Filename = filename;
        Stream = stream;
    }

    #endregion
}