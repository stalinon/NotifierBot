namespace NotifierBot.Infrastructure.Maintenance.Configuration.Impl;

/// <summary>
///     Креды супер админа
/// </summary>
public class SuperAdminCredentials : ICredentials
{
    private const string LOGIN = nameof(LOGIN);
    private const string PASSWORD = nameof(PASSWORD);
    
    /// <inheritdoc />
    public string Login { get; }

    /// <inheritdoc />
    public string? Password { get; }

    /// <inheritdoc cref="SuperAdminCredentials" />
    public SuperAdminCredentials()
    {
        Login = Environment.GetEnvironmentVariable(LOGIN) ?? "admin";
        Password = Environment.GetEnvironmentVariable(PASSWORD) ?? "admin";
    }
}