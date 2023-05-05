using System.Runtime.CompilerServices;
using Common.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class General : WindowConfig
{

    public bool Autostart { get; set; } = true;
    public bool DimScreenWhenNotActive { get; set; }
    public bool ShowBorderAlongBottomOfPrimaryScreen { get; set; }

    public string WallpaperUri { get; set; } = "";

    public bool ShowFiles { get; set; } = true;
    public bool ShowNotes { get; set; } = true;

    public override void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(Autostart))
            AppUtility.AutoStart.IsEnabled = Autostart;
    }

}
