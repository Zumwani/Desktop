using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenCalendar : Command
{

    public override void Execute() =>
        Process.Start(new ProcessStartInfo("https://calendar.google.com/calendar/u/0/r/month") { UseShellExecute = true });

}