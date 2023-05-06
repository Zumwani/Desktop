using Common.Utility;
using Desktop.Config;

namespace Desktop.Commands;

public class OpenCalendar : Command
{

    public override void Execute() =>
        FileUtility.Open(ConfigManager.DateAndTime.CalendarUri);

}
