using System.Windows;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class DesktopWindow : ConfigModule
{
    public double Left { get; set; } = SystemParameters.FullPrimaryScreenWidth / 2 - 400;
    public double Top { get; set; } = SystemParameters.FullPrimaryScreenHeight / 2 - 300;
    public double Width { get; set; } = 800;
    public double Height { get; set; } = 600;
}