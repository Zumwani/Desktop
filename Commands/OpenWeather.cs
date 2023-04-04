using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenWeather : Command
{

    public override void Execute() =>
        Process.Start(new ProcessStartInfo("https://openweathermap.org/city/2694893") { UseShellExecute = true });

}
