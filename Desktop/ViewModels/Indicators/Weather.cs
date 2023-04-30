using System;
using System.ComponentModel;
using System.Diagnostics;
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

    public RelayCommand ClickCommand { get; }
    public RelayCommand OpenPopupCommand { get; }
    public RelayCommand ReloadCommand { get; }
    public RelayCommand UrlClickCommand { get; }

    public double? Temperature { get; private set; }
    public Uri? Icon { get; private set; }

    public bool IsPopupOpen { get; set; }

    [SafeForDependencyAnalysis]
    public Settings.Weather.Config Config => Settings.Weather.Current.Value ??= new();

    [SafeForDependencyAnalysis]
    public string Url => WeatherUtility.Url;

    public Weather()
    {

        OpenPopupCommand = new(OpenPopup);
        UrlClickCommand = new(OpenApiUrl);

        ReloadCommand = new(() =>
        {
            Helper.Weather.Update();
            Update();
            IsPopupOpen = false;
        });

        ClickCommand = new(() =>
        {
            if (WeatherUtility.IsValid)
                OpenWebsiteUrl();
            else
                OpenPopup();
        });

        ((INotifyPropertyChanged)Config).PropertyChanged += (s, e) => OnPropertyChanged(nameof(Url));

    }

    void OpenApiUrl() =>
        _ = Process.Start(new ProcessStartInfo(WeatherUtility.Url) { UseShellExecute = true });

    void OpenWebsiteUrl() =>
        _ = Process.Start(new ProcessStartInfo(WeatherUtility.WebsiteUrl) { UseShellExecute = true });

    void OpenPopup() =>
        IsPopupOpen = true;

    public override void Update()
    {
        var weather = Helper.Weather.Value;
        Temperature = weather.Temperature;
        Icon = weather.Icon;
    }

}