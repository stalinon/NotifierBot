using NotifierBot.Infrastructure.Maintenance.Enums;

namespace NotifierBot.Infrastructure.Maintenance.Configuration.Impl;

/// <inheritdoc cref="IDatabaseConfiguration" />
internal sealed class DatabaseConfiguration : IDatabaseConfiguration
{
    private const string CONNECTION_STRING = nameof(CONNECTION_STRING);
    private const string DATABASE_MODE = nameof(DATABASE_MODE);
    
    /// <inheritdoc />
    public string ConnectionString { get; }

    /// <inheritdoc />
    public EnvironmentStatus DatabaseMode { get; }

    /// <inheritdoc cref="DatabaseConfiguration" />
    public DatabaseConfiguration()
    {
        ConnectionString = Environment.GetEnvironmentVariable(CONNECTION_STRING)!;

        var mode = Environment.GetEnvironmentVariable(DATABASE_MODE);
        DatabaseMode = string.IsNullOrEmpty(mode)
            ? EnvironmentStatus.USE_MOCK
            : Enum.Parse<EnvironmentStatus>(mode);
    }
}