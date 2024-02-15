namespace NotifierBot.Infrastructure.Maintenance.Configuration.Impl;

/// <inheritdoc cref="IDatabaseConfiguration" />
internal sealed class DatabaseConfiguration : IDatabaseConfiguration
{
    private const string CONNECTION_STRING = nameof(CONNECTION_STRING);
    
    /// <inheritdoc />
    public string ConnectionString { get; }

    /// <inheritdoc cref="DatabaseConfiguration" />
    public DatabaseConfiguration()
    {
        ConnectionString = Environment.GetEnvironmentVariable(CONNECTION_STRING)!;
    }
}