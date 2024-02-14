using Microsoft.EntityFrameworkCore;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Конфигурируемая сущность
/// </summary>
public interface IConfigurable
{
    /// <summary>
    ///     Установить конфигурацию сущности
    /// </summary>
    static abstract void Setup(ModelBuilder modelBuilder);
}