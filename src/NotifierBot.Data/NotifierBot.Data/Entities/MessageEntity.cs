using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotifierBot.Data.Entities;

/// <summary>
///     Сущность сообщения
/// </summary>
public sealed class MessageEntity : IHasId, IEntityLifecycle, IConfigurable
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
    ///     Идентификатор отправителя
    /// </summary>
    [Column("sender_id"), ForeignKey(nameof(Sender))]
    public long SenderId { get; set; }

    /// <summary>
    ///     Отправитель
    /// </summary>
    public SenderEntity Sender { get; set; } = default!;

    /// <summary>
    ///     Идентификатор получателя
    /// </summary>
    [Column("recipient_id"), ForeignKey(nameof(Recipient))]
    public long RecipientId { get; set; }

    /// <summary>
    ///     Получатель
    /// </summary>
    public RecipientEntity Recipient { get; set; } = default!;

    /// <summary>
    ///     Тело сообщения
    /// </summary>
    [Column("content", TypeName = "jsonb")]
    public MessageContent Content { get; set; } = default!;

    /// <inheritdoc />
    public static void Setup(ModelBuilder modelBuilder)
    { }   
}