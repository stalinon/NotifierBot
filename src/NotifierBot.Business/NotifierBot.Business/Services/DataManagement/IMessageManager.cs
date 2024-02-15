using NotifierBot.Infrastructure.Models.Api;

namespace NotifierBot.Business.Services.DataManagement;

/// <summary>
///     Сервис управления данными сообщений
/// </summary>
public interface IMessageManager : IDataManager<Message>
{ }