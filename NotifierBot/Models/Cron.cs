namespace NotifierBot.Models;

/// <summary>
/// CRON-выражение
/// </summary>
public readonly struct Cron
{
    #region Factory

    /// <summary>
    /// Ежесекундно
    /// </summary>
    public static string Secondly()
        => "* * * * * ? *";

    /// <summary>
    /// Ежесекундно с смещением
    /// </summary>
    public static string Secondly(int every)
        => $"0/{every} * * * * ? *";

    /// <summary>
    /// Ежеминутно
    /// </summary>
    public static string Minutely(int second = 0)
        => $"{second} * * * * ? *";

    /// <summary>
    /// По указанным минутам
    /// </summary>
    public static string AtMinutes(int[] minutes)
        => $"0 {string.Join(',', minutes)} * ? * * *";

    /// <summary>
    /// Каждые every минут
    /// </summary>
    public static string EveryNthMinute(int everyMinutes, int seconds = 0)
        => $"{seconds} 0/{everyMinutes} * * * ? *";

    /// <summary>
    /// Каждые minutes минут с учетом смещения назад в offset минут.
    /// Например, offset = 2, minutes = 15, значит время выполнения будет 9:58, 10:13, 10:28, 10:43, 10:58, 11:13 и т.д.
    /// </summary>
    public static string EveryNthMinuteOfHourWithOffset(int minutes, int offset)
        => $"0 {minutes - offset}/{minutes} * * * ?";

    /// <summary>
    /// Ежечасно
    /// </summary>
    public static string Hourly(int minute = 0, int second = 0)
        => $"{second} {minute} * * * ? *";

    /// <summary>
    /// Каждые <paramref name="hour"/> часов
    /// </summary>
    public static string EveryNthHour(int hour, int minutes = 0, int seconds = 0) =>
        $"{seconds} {minutes} 0/{hour} 1/1 * ? *";

    /// <summary>
    /// Ежедневно
    /// </summary>
    public static string Daily(int hour = 0, int minute = 0, int second = 0)
        => $"{second} {minute} {hour} * * ? *";

    /// <summary>
    /// Каждые <paramref name="day"/> часов
    /// </summary>
    public static string EveryNthDay(int day, int hour = 0, int minutes = 0, int seconds = 0) =>
        $"{seconds} {minutes} 0/{hour} 1/1 * ? *";

    /// <summary>
    /// Еженедельно
    /// </summary>
    public static string Weekly(DayOfWeek dayOfWeek = DayOfWeek.Monday, int hour = 0, int minute = 0, int second = 0)
    {
        var dayName = dayOfWeek.ToString().Substring(0, 3).ToUpperInvariant();
        return $"{second} {minute} {hour} ? * {dayName} *";
    }

    /// <summary>
    /// Ежемесячно
    /// </summary>
    public static string Monthly(int day = 1, int hour = 0, int minute = 0, int second = 0)
        => $"{second} {minute} {hour} {day} * ? *";

    /// <summary>
    /// Ежегодно
    /// </summary>
    public static Cron Yearly(int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0)
        => $"{second} {minute} {hour} {day} {month} ? *";

    /// <summary>
    /// Никогда
    /// </summary>
    public static Cron Never() => Yearly(2, 31);

    #endregion

    /// <summary>
    /// .ctor
    /// </summary>
    private Cron(string value)
    {
        _value = value;
    }

    /// <summary>
    /// Выражение
    /// </summary>
    private readonly string _value = default!;

    public static implicit operator Cron(string str)
    {
        return new Cron(str);
    }

    public static implicit operator string(Cron cron)
    {
        return cron._value;
    }

    /// <inheritdoc />
    public override string ToString() => _value;

    /// <summary>
    /// Сравнить
    /// </summary>
    public bool Equals(Cron other) => _value == other._value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Cron other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    public static bool operator ==(Cron left, Cron right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Cron left, Cron right)
    {
        return !(left == right);
    }
}