using Common.Utility;
using Desktop.Utility;

namespace Desktop.Commands;

public class OpenRecycleBin : Command
{

    public override void Execute() =>
        FileUtility.Open("shell:RecycleBinFolder");

}