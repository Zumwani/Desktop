using Common.Utility;
using Desktop.Utility;

namespace Desktop.Commands;

public class OpenCalendar : Command
{

    public override void Execute() =>
        FileUtility.Open(Settings.CalendarUri.Current.Value!);

}
