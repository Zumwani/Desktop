using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenDownloads : Command
{

    public override void Execute() =>
        Process.Start(new ProcessStartInfo("shell:downloads") { UseShellExecute = true });

}
