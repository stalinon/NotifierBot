using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Сущность отправителя
/// </summary>
[Table("senders")]
public sealed class SenderEntity : IHasId, IEntityLifecycle, IConfigurable
{
    /// <inheritdoc />
    [Column("id")]
    public long Id { get; set; }
    
    /// <inheritdoc />
    [Column("created")]
    public DateTime Created { get; set; }

    /// <inheritdoc />
    [Column("updated")]
    public DateTime Updated { get; set; }

    /// <summary>
    ///     Тип отправителя
    /// </summary>
    [Column("type")]
    public SenderType Type { get; set; }

    /// <summary>
    ///     Название отправителя
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    ///     Токен доступа
    /// </summary>
    [Column("token")]
    public string Token { get; set; } = default!;

    /// <inheritdoc />
    public static void Setup(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<SenderEntity>();
        entityBuilder.HasIndex(e => e.Type);
        entityBuilder.HasIndex(e => e.Name).IsUnique();
    }
}