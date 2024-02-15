using Quartz;

namespace NotifierBot.Business.Impl.Services.Schedule;

/// <summary>
///     Повторяющаяся задача
/// </summary>
internal sealed class RecurringJob : IJob
{
    public const string ActionKey = nameof(ActionKey);

    public async Task Execute(IJobExecutionContext context)
    {
        if (!context.MergedJobDataMap.TryGetValue(ActionKey, out var obj))
        {
            return;
        }

        if (obj is not Func<Task> func)
        {
            return;
        }

        await func.Invoke();
    }
}