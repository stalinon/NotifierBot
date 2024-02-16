
using NotifierBot.Infrastructure.Maintenance.Configuration;

namespace NotifierBot.Presentation.Models;

/// <summary>
///     Модель авторизации
/// </summary>
public sealed class AuthModel(string login, string password) : ICredentials
{
    /// <summary>
    ///     Логин
    /// </summary>
    public string Login { get; set; } = login;

    /// <summary>
    ///     Пароль
    /// </summary>
    public string Password { get; set; } = password;
}