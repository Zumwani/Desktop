using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenRecycleBin : Command
{

    public override void Execute() =>
        Process.Start(new ProcessStartInfo("shell:RecycleBinFolder") { UseShellExecute = true });

}