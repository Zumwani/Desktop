using System;

namespace Desktop.Models;

public enum Unit
{
    Milliseconds,
    Seconds,
    Minutes,
    Hours
}

public struct Duration
{

    public double Value { get; set; }
    public Unit Unit { get; set; }

    public TimeSpan GetTimeSpan() =>
        Unit switch
        {
            Unit.Milliseconds => TimeSpan.FromMilliseconds(Value),
            Unit.Seconds => TimeSpan.FromSeconds(Value),
            Unit.Minutes => TimeSpan.FromMinutes(Value),
            Unit.Hours => TimeSpan.FromHours(Value),
            _ => default,
        };

    public static implicit operator TimeSpan(Duration duration) =>
        duration.GetTimeSpan();

    public static Duration FromMilliseconds(double value) => new() { Value = value, Unit = Unit.Milliseconds };
    public static Duration FromSeconds(double value) => new() { Value = value, Unit = Unit.Seconds };
    public static Duration FromMinutes(double value) => new() { Value = value, Unit = Unit.Minutes };
    public static Duration FromHours(double value) => new() { Value = value, Unit = Unit.Hours };

}
