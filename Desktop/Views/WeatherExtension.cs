using System;
using System.Windows.Data;
using Desktop.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

public class WeatherExtension : Binding
{

    [NotifyPropertyChanged]
    public class Helper
    {
        public Models.Weather Weather { get; private set; }
        public Helper() => ActionUtility.Invoke(TimeSpan.FromMinutes(1), Update);
        public async void Update() => Weather = await WeatherUtility.Get();
    }

    static readonly Helper helper = new();

    public static void Update() => helper.Update();

    public WeatherExtension()
    {
        Source = helper;
        Path = new(nameof(Helper.Weather));
        Mode = BindingMode.OneWay;
    }

    public WeatherExtension(string path)
    {
        Source = helper;
        Path = new(nameof(Helper.Weather) + "." + path);
        Mode = BindingMode.OneWay;
    }

}