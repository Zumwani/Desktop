﻿using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class CpuTemperature : TrackerIndicator
{

    public double? Value { get; private set; }

    public override void Update() =>
        Value = IndicatorUtility.Tracker.Value?.CpuTemperature;

}