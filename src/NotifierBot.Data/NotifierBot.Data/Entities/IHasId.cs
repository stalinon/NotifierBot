namespace NotifierBot.Data.Entities;

/// <summary>
///     Сущность, содержащая идентификатор
/// </summary>
public interface IHasId
{
    /// <summary>
    ///     Идентификатор записи в БД
    /// </summary>
    long Id { get; set; }
}