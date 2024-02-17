
namespace NotifierBot.Presentation.Models;

/// <summary>
///     Модель авторизации
/// </summary>
public sealed class AuthModel
{
    /// <summary>
    ///     Логин
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    ///     Пароль
    /// </summary>
    public string? Password { get; set; }
}