using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenTaskManager : Command
{

    public override void Execute() =>
        Process.Start(new ProcessStartInfo("taskmgr") { UseShellExecute = true });

}
