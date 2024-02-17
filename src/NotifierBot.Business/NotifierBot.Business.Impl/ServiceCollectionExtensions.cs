using Microsoft.Extensions.DependencyInjection;
using NotifierBot.Business.Impl.Services;
using NotifierBot.Business.Impl.Services.DataManagement;
using NotifierBot.Business.Services;
using NotifierBot.Business.Services.DataManagement;
using NotifierBot.Data.Impl;

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
        
        services.AddScoped<ISenderManager, SenderManager>();
        services.AddScoped<IRecipientManager, RecipientManager>();
        services.AddScoped<IMessageManager, MessageManager>();
        services.AddScoped<IScheduleManager, ScheduleManager>();
        services.AddSingleton<IDataManagementFacade, DataManagementFacade>();
        services.AddSingleton<ISenderFactory, SenderFactory>();
        services.AddScoped<IScheduler, Scheduler>();
        
        return services;
    }

    /// <summary>
    ///     Запустить все активные расписания
    /// </summary>
    public static async Task StartAllActiveSchedulesAsync(this IServiceProvider serviceProvider)
    {
        var dataManager = serviceProvider.GetRequiredService<IDataManagementFacade>();
        var scheduler = serviceProvider.GetRequiredService<IScheduler>();
        var schedules = await dataManager.Schedules.ListAsync();
        var activeSchedules = schedules.Where(s => s.IsActive).ToArray();

        foreach (var schedule in activeSchedules)
        {
            await scheduler.ScheduleMessageAsync(schedule, CancellationToken.None);
        }
    }
}