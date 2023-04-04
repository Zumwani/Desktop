using System.Diagnostics;
using Common.Utility;

namespace Desktop.Commands;

public class OpenWeather : Command
{

    public override void Execute()
    {
        if (Settings.Weather.Current.Value?.IsValid ?? false)
            _ = Process.Start(new ProcessStartInfo("https://openweathermap.org/find?q=" + Settings.Weather.Current.Value.SearchLocation) { UseShellExecute = true });
    }
}
