global using Common.Utility;
global using Desktop.Utility;
global using RelayCommand = Common.Utility.RelayCommand;
using System.Windows;
using Desktop.Config;
using Desktop.Windows;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
namespace Desktop;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {

        if (AppUtility.IsPrimaryInstance())
        {

            ServerUtility.Restart();

            MainWindow = new DesktopWindow();
            IdleWindowSecondary.Initialize();
            _ = new IdleWindowPrimary();

            if (ConfigManager.General.IsFirstOpen)
            {
                ConfigManager.General.IsFirstOpen = false;
                ConfigManager.General.IsEditMode = true;
                SettingsWindow.Open();
            }

        }
        else
            Shutdown();

    }

}
