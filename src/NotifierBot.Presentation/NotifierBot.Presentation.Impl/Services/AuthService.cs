using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using NotifierBot.Infrastructure.Maintenance;
using NotifierBot.Infrastructure.Maintenance.Exceptions;
using NotifierBot.Presentation.Services;
using ICredentials = NotifierBot.Infrastructure.Maintenance.Configuration.ICredentials;

namespace NotifierBot.Presentation.Impl.Services;

/// <summary>
///     Сервис для входа в систему и выхода из нее
/// </summary>
internal sealed class AuthService : IAuthService
{
    private const string AuthScheme = "Basic";

    private readonly ICredentials _singletonCreds;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <inheritdoc cref="AuthService"/>
    public AuthService(
        IHttpContextAccessor httpContextAccessor
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _singletonCreds = Config.GetSuperAdminCredentials();
    }

    /// <summary>
    ///     Выполнить авторизацию.
    ///     Возвращает null, если авторизация не удалась.
    /// </summary>
    public async Task LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        if (email != _singletonCreds.Login || password != _singletonCreds.Password)
        {
            throw new ErrorException(HttpStatusCode.Forbidden, $"Неверные креды.");
        }

        await _httpContextAccessor.HttpContext!.AuthenticateAsync(AuthScheme);
    }

    /// <summary>
    ///     Выполнить выход из системы
    /// </summary>
    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(AuthScheme);
    }

    private static ClaimsIdentity CreateUserIdentity(string email)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, AuthScheme);
        return claimsIdentity;
    }
}