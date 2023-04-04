using System;
using System.IO;
using System.Windows;
using Common.Utility;

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
namespace Desktop;

public partial class App : Application
{

    protected override void OnStartup(StartupEventArgs e)
    {

        if (AppUtility.IsSecondaryInstance())
            return;

        CreateStartmenuShortcut();
        AppUtility.AutoStart.Enable();
        ServerUtility.Initialize();

        MainWindow = new DesktopWindow();
        _ = new IdleWindow();

    }

    protected override void OnExit(ExitEventArgs e)
    {
        ServerUtility.Stop();
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
