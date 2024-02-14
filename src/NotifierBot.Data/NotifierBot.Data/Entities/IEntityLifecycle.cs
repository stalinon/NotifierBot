namespace NotifierBot.Data.Entities;

/// <summary>
///     Описывает жизненный цикл записи
///     в базе данных через временные метки
///     создания и обновления.
/// </summary>
public interface IEntityLifecycle
{
    /// <summary>
    ///     Дата создания записи
    /// </summary>
    DateTime Created { get; internal set; }
    
    /// <summary>
    ///     Дата обновления записи
    /// </summary>
    DateTime Updated { get; internal set; }

    /// <summary>
    ///     Установить даты отслеживания жизненного цикла
    /// </summary>
    void SetLifecycle()
    {
        var now = DateTime.UtcNow;
        Updated = now;
        if (Created == DateTime.MinValue)
        {
            Created = now;
        }
    }
}