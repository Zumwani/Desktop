﻿using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class IdleMode : WindowConfig
{

    public override bool IsIdle => true;

    public bool IsEnabled { get; set; } = false;
    public Duration DelayBeforeOpening { get; set; } = Duration.FromMinutes(5);
    public bool DisableDelayInDebugMode { get; set; } = true;

    public bool DimPrimaryScreen { get; set; } = true;
    public bool DisplayClockOnPrimary { get; set; } = true;
    public bool DisplayNotificationsOnPrimary { get; set; } = true;

}
