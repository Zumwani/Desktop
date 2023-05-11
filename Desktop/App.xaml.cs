global using Common.Utility;
global using Desktop.Utility;
global using RelayCommand = Common.Utility.RelayCommand;
using System.Windows;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
namespace Desktop;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {

        if (AppUtility.IsPrimaryInstance())
        {
            MainWindow = new DesktopWindow();
            _ = new IdleWindow();
        }
        else
            Shutdown();

    }

}
