using Common.Utility;
using Desktop.Utility;

namespace Desktop.Commands;

public class ConfigureDateAndTime : Command
{

    public override void Execute() =>
        FileUtility.Open("ms-settings:dateandtime");

}
