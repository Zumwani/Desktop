using Common.Utility;
using Desktop.Utility;

namespace Desktop.Commands;

public class OpenTaskManager : Command
{

    public override void Execute() =>
        FileUtility.Open("taskmgr");

}
