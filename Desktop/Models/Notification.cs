using System;
using PostSharp.Patterns.Model;

namespace Desktop.Models;

[NotifyPropertyChanged]
public class Notification
{

    public Notification(string content)
    {
        Content = content;
        IsPermanent = true;
        EndTime = DateTime.Today.AddDays(1);
    }

    public Notification(string content, TimeSpan duration)
    {
        Content = content;
        Duration = duration;
        CreationTime = DateTime.Now;
        EndTime = CreationTime + duration;
    }

    public string? Header { get; set; }
    public string Content { get; set; }

    public bool IsPermanent { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }

}
