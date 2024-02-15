using Quartz;
using Quartz.Impl;

namespace NotifierBot.Business.Impl.Services;

/// <summary>
///     Синглтон-реализация фабрики планировщика
/// </summary>
internal static class SingletonScheduler
{
    private static IScheduler? _scheduler;
    private static readonly object Lock = new();

    /// <summary>
    ///     Создать экземпляр планировщика
    /// </summary>
    public static IScheduler Get()
    {
        if (_scheduler == null)
        {
            lock (Lock)
            {
                if (_scheduler == null)
                {
                    var factory = new StdSchedulerFactory();
                    _scheduler = factory.GetScheduler().Result;
                }
            }
        }

        return _scheduler;
    }
}
