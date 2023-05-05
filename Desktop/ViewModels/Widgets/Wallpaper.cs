using Desktop.Config;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Wallpaper
{

    public Config.General GeneralConfig { get; } = ConfigManager.General;
    public WindowConfig? Config { get; set; }

    public bool IsChangingWallpaper { get; set; }

}
