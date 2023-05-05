using System;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class Weather : IntervalViewModel
{

    public double? Temperature { get; private set; }
    public Uri? Icon { get; private set; }

    public RelayCommand OpenWebpageCommand { get; } = new(() => FileUtility.Open(WeatherUtility.WebsiteUrl));

    public override void Update()
    {
        var weather = IndicatorUtility.Weather.Value;
        Temperature = weather.Temperature;
        Icon = weather.Icon;
    }

}