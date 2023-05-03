using Common.Utility;
using Desktop.Config;
using Desktop.Utility;

namespace Desktop.Commands;

public class OpenCalendar : Command
{

    public override void Execute() =>
        FileUtility.Open(ConfigManager.DateAndTime.CalendarUri);

}
