using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Weather : ConfigModule
{

    public static Weather Instance { get; } = new();

    public Duration UpdateInterval { get; set; } = Duration.FromMinutes(1);

    public string ApiKey { get; set; } = "";
    public string SearchLocation { get; set; } = "";
    public WeatherUnit Unit { get; set; } = WeatherUnit.Metric;
    public bool UseFeelsLikeTemperature { get; set; } = true;

}
