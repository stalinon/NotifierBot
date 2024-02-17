using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Business.Impl.Services;
using NotifierBot.Business.Impl.Services.DataManagement;
using NotifierBot.Business.Services;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data.Impl;
using NotifierBot.Infrastructure.Maintenance.Enums;

namespace NotifierBot.Business.Impl;

/// <summary>
///     Расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Добавить слой бизнес-логики
    /// </summary>
    public static IServiceCollection SetupBusinessLayer(this IServiceCollection services)
    {
        services.SetupDataAccessLayer();
        
        services.AddSingleton<ISenderManager, SenderManager>();
        services.AddSingleton<IRecipientManager, RecipientManager>();
        services.AddSingleton<IMessageManager, MessageManager>();
        services.AddSingleton<IScheduleManager, ScheduleManager>();
        services.AddSingleton<IDataManagementFacade, DataManagementFacade>();
        services.AddSingleton<ISenderFactory, SenderFactory>();
        services.AddScoped<IScheduler, Scheduler>();
        return services;
    }
}