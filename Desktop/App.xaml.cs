global using Common.Utility;
global using Desktop.Utility;
global using RelayCommand = Common.Utility.RelayCommand;
using System;
using System.IO;
using System.Windows;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
namespace Desktop;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {

        if (AppUtility.IsSecondaryInstance())
            return;

        CreateStartmenuShortcut();
        ServerUtility.Initialize();

        MainWindow = new DesktopWindow();
        _ = new IdleWindow();

    }

    static void CreateStartmenuShortcut()
    {

        if (Environment.ProcessPath is null)
            return;

        using StreamWriter writer = new(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs\\Desktop.url");

        writer.WriteLine("[InternetShortcut]");
        writer.WriteLine("URL=file:///" + Environment.ProcessPath);
        writer.WriteLine("IconIndex=0");

        var icon = Environment.ProcessPath.Replace('\\', '/');
        writer.WriteLine("IconFile=" + icon);
        writer.Flush();

    }

}
