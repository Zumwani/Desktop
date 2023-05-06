using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class General : WindowConfig
{

    public bool DimScreenWhenNotActive { get; set; }
    public bool ShowBorderAlongBottomOfPrimaryScreen { get; set; }

    public string WallpaperUri { get; set; } = "";

    public bool ShowFiles { get; set; } = true;
    public bool ShowNotes { get; set; } = true;

}
