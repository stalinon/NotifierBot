using System.Net;

namespace NotifierBot.Infrastructure.Maintenance.Exceptions;

/// <summary>
///     Общее исключение
/// </summary>
public class ErrorException(HttpStatusCode code, string message) : Exception($"{code} : {message}")
{
    /// <summary>
    ///     Код исключения
    /// </summary>
    public HttpStatusCode Code { get; } = code;
}