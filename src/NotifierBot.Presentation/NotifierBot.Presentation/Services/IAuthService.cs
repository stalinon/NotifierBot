namespace NotifierBot.Presentation.Services;

/// <summary>
///     Сервис для входа в систему и выхода из нее
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Выполнить авторизацию
    /// </summary>
    Task LoginAsync(string email, string password, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Выполнить выход из системы
    /// </summary>
    Task LogoutAsync(CancellationToken cancellationToken = default);
}