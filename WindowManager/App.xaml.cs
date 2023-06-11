using System.Windows;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
namespace WindowManager;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {
        var w = new WindowArea();
        w.Show();
    }

}
