using System;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Date : IntervalViewModel
{

    public DateTime Value { get; private set; }

    public override void Update() =>
        Value = DateTime.Now;

}
