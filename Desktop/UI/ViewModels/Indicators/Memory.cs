﻿using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Memory : TrackerIndicator
{

    public double? UsedMemory { get; private set; }
    public double? AvailableMemory { get; private set; }

    public override void Update()
    {
        UsedMemory = IndicatorUtility.Tracker.Value?.UsedMemory;
        AvailableMemory = IndicatorUtility.Tracker.Value?.AvailableMemory;
    }

}