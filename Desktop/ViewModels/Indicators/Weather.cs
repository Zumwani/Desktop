using System;
using System.Windows.Markup;
using Desktop.Utility;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

public class WeatherUnitEnumTypeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider) => typeof(WeatherUnit);
}

[NotifyPropertyChanged]
public class Weather : IntervalViewModel
{

    public double? Temperature { get; private set; }
    public Uri? Icon { get; private set; }

    public override void Update()
    {
        var weather = Helper.Weather.Value;
        Temperature = weather.Temperature;
        Icon = weather.Icon;
    }

}