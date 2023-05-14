using System;
using System.Linq;
using PostSharp.Patterns.Model;

namespace Desktop.Models;

[NotifyPropertyChanged]
public class Notification
{

    public Notification(string content, TimeSpan? duration = null)
    {

        Content = content;
        Duration = duration;

        Header ??= content.Contains('\n') ? content.Split('\n', StringSplitOptions.RemoveEmptyEntries).First() : content;

        IsPermanent = duration is null;
        ResetTimer();

    }

    public int DuplicateHeaderCount { get; set; } = 1;
    public string? Header { get; set; }
    public bool IsVisible { get; set; } = true;
    public string Content { get; set; }

    public bool IsPermanent { get; set; }
    public DateTime? CreationTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? Duration { get; set; }

    public void Collapse()
    {
        DuplicateHeaderCount += 1;
        ResetTimer();
    }

    public void ResetTimer()
    {
        if (IsPermanent)
            EndTime = DateTime.Today.AddDays(1);
        else
        {
            CreationTime = DateTime.Now;
            EndTime = CreationTime + Duration!.Value;
        }
    }

}
