using Common.Utility;

namespace Desktop.Commands;

public class Quit : Command
{

    public override void Execute() =>
        App.Current.Shutdown();

}
