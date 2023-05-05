using System;

namespace Desktop.Models;

public enum TimeUnit
{
    Milliseconds,
    Seconds,
    Minutes,
    Hours
}

public struct Duration
{

    public double Value { get; set; }
    public TimeUnit Unit { get; set; }

    public TimeSpan GetTimeSpan() =>
        Unit switch
        {
            TimeUnit.Milliseconds => TimeSpan.FromMilliseconds(Value),
            TimeUnit.Seconds => TimeSpan.FromSeconds(Value),
            TimeUnit.Minutes => TimeSpan.FromMinutes(Value),
            TimeUnit.Hours => TimeSpan.FromHours(Value),
            _ => default,
        };

    public static implicit operator TimeSpan(Duration duration) =>
        duration.GetTimeSpan();

    public static Duration FromMilliseconds(double value) => new() { Value = value, Unit = TimeUnit.Milliseconds };
    public static Duration FromSeconds(double value) => new() { Value = value, Unit = TimeUnit.Seconds };
    public static Duration FromMinutes(double value) => new() { Value = value, Unit = TimeUnit.Minutes };
    public static Duration FromHours(double value) => new() { Value = value, Unit = TimeUnit.Hours };

}
