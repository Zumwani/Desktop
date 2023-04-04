using System;
using PostSharp.Patterns.Model;

namespace Desktop.Models;

public enum TimeUnit
{
    Seconds,
    Minutes,
    Hours
}

[NotifyPropertyChanged]
public class Note
{

    public string Content { get; set; } = "New note";

    public bool IsHidden { get; set; }
    public bool HasNotification { get; set; }

    public bool PermanentNotification { get; set; }
    public DateTime NotifyAt { get; set; }
    public int NotifyDurationValue { get; set; } = 5;
    public TimeUnit NotifyDurationUnit { get; set; }

    public TimeSpan NotifyDuration =>
        NotifyDurationUnit switch
        {
            TimeUnit.Seconds => TimeSpan.FromSeconds(NotifyDurationValue),
            TimeUnit.Minutes => TimeSpan.FromMinutes(NotifyDurationValue),
            TimeUnit.Hours => TimeSpan.FromHours(NotifyDurationValue),
            _ => TimeSpan.Zero,
        };

    public bool NotifyOnMonday { get; set; }
    public bool NotifyOnTuesday { get; set; }
    public bool NotifyOnWednesday { get; set; }
    public bool NotifyOnThursday { get; set; }
    public bool NotifyOnFriday { get; set; }
    public bool NotifyOnSaturday { get; set; }
    public bool NotifyOnSunday { get; set; }

}
