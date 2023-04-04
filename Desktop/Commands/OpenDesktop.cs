using System;
using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenDesktop : Command
{

    public override void Execute() =>
        Process.Start(new ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)) { UseShellExecute = true });

}