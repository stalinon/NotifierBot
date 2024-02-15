namespace NotifierBot.Business.Impl.Mapping;

/// <summary>
///     Расширения маппера
/// </summary>
internal static class MapperExtensions
{
    /// <summary>
    ///     Маппить коллекцию сущностей в модели
    /// </summary>
    public static IEnumerable<TModel> Map<TEntity, TModel>(this Mapper<TEntity, TModel> mapper, IEnumerable<TEntity> entities) =>
        entities.Select(mapper.Map);
    
    /// <summary>
    ///     Маппить коллекцию сущностей в модели
    /// </summary>
    public static TModel[] Map<TEntity, TModel>(this Mapper<TEntity, TModel> mapper, TEntity[] entities) =>
        mapper.Map((IEnumerable<TEntity>)entities).ToArray();
    
    /// <summary>
    ///     Маппить коллекцию сущностей в модели
    /// </summary>
    public static List<TModel> Map<TEntity, TModel>(this Mapper<TEntity, TModel> mapper, List<TEntity> entities) =>
        mapper.Map((IEnumerable<TEntity>)entities).ToList();
    
    /// <summary>
    ///     Маппить коллекцию моделей в сущности
    /// </summary>
    public static IEnumerable<TEntity> Map<TEntity, TModel>(this Mapper<TEntity, TModel> mapper, IEnumerable<TModel> models) =>
        models.Select(mapper.Map);
    
    /// <summary>
    ///     Маппить коллекцию моделей в сущности
    /// </summary>
    public static TEntity[] Map<TEntity, TModel>(this Mapper<TEntity, TModel> mapper, TModel[] models)
        => mapper.Map((IEnumerable<TModel>)models).ToArray();
    
    /// <summary>
    ///     Маппить коллекцию моделей в сущности
    /// </summary>
    public static List<TEntity> Map<TEntity, TModel>(this Mapper<TEntity, TModel> mapper, List<TModel> models)
        => mapper.Map((IEnumerable<TModel>)models).ToList();
}