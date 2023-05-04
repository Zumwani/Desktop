﻿using System.Windows.Media;
using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class IdleMode : SharedVisibilityConfig
{
    public bool IsEnabled { get; set; } = false;
    public Duration DelayBeforeOpening { get; set; } = Duration.FromMinutes(5);
    public bool DisableDelayInDebugMode { get; set; } = true;
    public Stretch WallpaperStretch { get; set; }
}
