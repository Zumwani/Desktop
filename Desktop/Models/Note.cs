using System;
using System.Text.Json.Serialization;
using PostSharp.Patterns.Model;

namespace Desktop.Models;

[NotifyPropertyChanged]
public class Note
{

    public string Content { get; set; } = "New note";

    public bool IsHidden { get; set; }
    public bool HasNotification { get; set; }

    public bool PermanentNotification { get; set; }
    public DateTime NotifyAt { get; set; }
    public Duration NotifyDuration { get; set; } = Duration.FromSeconds(5);

    [JsonPropertyName("mon")] public bool NotifyOnMonday { get; set; }
    [JsonPropertyName("tue")] public bool NotifyOnTuesday { get; set; }
    [JsonPropertyName("wed")] public bool NotifyOnWednesday { get; set; }
    [JsonPropertyName("thu")] public bool NotifyOnThursday { get; set; }
    [JsonPropertyName("fri")] public bool NotifyOnFriday { get; set; }
    [JsonPropertyName("sat")] public bool NotifyOnSaturday { get; set; }
    [JsonPropertyName("sun")] public bool NotifyOnSunday { get; set; }

}
