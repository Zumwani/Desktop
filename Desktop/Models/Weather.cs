using System;

namespace Desktop.Models;

public enum WeatherUnit
{
    Metric, Imperial
}

public struct Weather
{
    public Uri? Icon { get; set; }
    public double? Temperature { get; set; }
}
