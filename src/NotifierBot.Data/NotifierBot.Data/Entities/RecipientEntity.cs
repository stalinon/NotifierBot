using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NotifierBot.Infrastructure.Maintenance.Configuration;
using NotifierBot.Infrastructure.Models.Enums;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Сущность получателя
/// </summary>
[Table("recipients")]
public sealed class RecipientEntity : IHasId, IEntityLifecycle, IConfigurable, IRecipientSettings
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

    /// <inheritdoc />
    [Column("external_id")]
    public string ExternalIdentifier { get; set; } = default!;

    /// <summary>
    ///     Тип получателя
    /// </summary>
    [Column("type")]
    public RecipientType Type { get; set; }

    /// <summary>
    ///     Название получателя
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = default!;

    /// <inheritdoc />
    public static void Setup(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<RecipientEntity>();
        entityBuilder.HasIndex(e => e.Type);
        entityBuilder.HasIndex(e => e.Name);
    }   
}