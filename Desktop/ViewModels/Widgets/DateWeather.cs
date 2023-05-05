using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class DateWeather : ViewModel
{

    public WindowConfig? Config { get; set; }

    public Date Date { get; } = new();
    public Weather Weather { get; } = new();

}
