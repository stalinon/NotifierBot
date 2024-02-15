namespace NotifierBot.Business.Impl.Mapping;

/// <summary>
///     Клас-маппер
/// </summary>
internal abstract class Mapper<TEntity, TModel>
{
    /// <summary>
    ///     Маппить сущность в модель
    /// </summary>
    public abstract TModel Map(TEntity entity);
    
    /// <summary>
    ///     Маппить модель в сущность
    /// </summary>
    public abstract TEntity Map(TModel model);
    
    /// <summary>
    ///     Маппить сущность в модель
    /// </summary>
    public abstract TModel Map(TEntity entity, TModel model);
    
    /// <summary>
    ///     Маппить модель в сущность
    /// </summary>
    public abstract TEntity Map(TModel model, TEntity entity);
}