namespace NotifierBot.Business.Services.DataManagement;

/// <summary>
///     Сервис управления данными
/// </summary>
public interface IDataManager<TModel>
    where TModel : class, new()
{
    /// <summary>
    ///     Создать
    /// </summary>
    Task<TModel> CreateAsync(
        TModel model,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Читать
    /// </summary>
    Task<TModel?> ReadAsync(
        long id,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить
    /// </summary>
    Task<TModel> UpdateAsync(
        TModel model,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить
    /// </summary>
    Task<TModel> DeleteAsync(
        long id,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список
    /// </summary>
    Task<TModel[]> ListAsync(
        long? limit = null,
        long? offset = null,
        CancellationToken cancellationToken = default);
}