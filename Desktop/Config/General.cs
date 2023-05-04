using System.Runtime.CompilerServices;
using System.Windows.Media;
using Common.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class General : SharedVisibilityConfig
{

    public bool Autostart { get; set; } = true;
    public bool DimScreenWhenNotActive { get; set; }
    public bool ShowBorderAlongBottomOfPrimaryScreen { get; set; }

    public string WallpaperUri { get; set; } = "";
    public Stretch WallpaperStretch { get; set; }

    public bool ShowFiles { get; set; } = true;
    public bool ShowNotes { get; set; } = true;

    public override void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(Autostart))
            AppUtility.AutoStart.IsEnabled = Autostart;
    }

}
