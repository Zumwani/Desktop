using System;
using Desktop.Config;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Date : DateTimeIndicator
{

    public string Value { get; private set; } = "--";

    public override void Update()
    {
        var format = ConfigManager.DateAndTime.UseWindowsFormatForDate ? "d" : ConfigManager.DateAndTime.DateFormat;
        Value = DateTime.Now.ToString(format);
    }

}
