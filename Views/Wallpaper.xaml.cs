using System.Windows.Controls;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

[NotifyPropertyChanged]
public partial class Wallpaper : UserControl
{

    public bool IsChangingWallpaper { get; set; }
    public bool IsIdle { get; set; }

    public Wallpaper()
    {
        InitializeComponent();
        Commands.ChangeWallpaper.SetHandler(() => IsChangingWallpaper = true);
    }

}
