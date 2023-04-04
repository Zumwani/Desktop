using Common.Utility;

namespace Desktop.Commands;

public class RestartSystemStatusServer : Command
{

    public override void Execute() =>
        ServerUtility.Restart();

}
