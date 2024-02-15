namespace NotifierBot.Infrastructure.Models.Abstractions;

/// <summary>
///     Модель с идентификатором
/// </summary>
public interface IModelWithId
{
    /// <summary>
    ///     Идентификатор в БД
    /// </summary>
    long Id { get; }
}