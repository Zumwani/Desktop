using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class DateTimeWeather : ViewModel
{

    public WindowConfig? Config { get; set; }

    public Date Date { get; } = new();
    public Time Time { get; } = new();
    public Weather Weather { get; } = new();

}
