using System;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Views;

public partial class Weather : UserControl
{

    public bool IsIdle { get; set; }

    public Weather() =>
        InitializeComponent();

    void MenuItem_Click(object sender, RoutedEventArgs e) =>
        locationPopup.IsOpen = true;

    void LocationPopup_Closed(object sender, EventArgs e) =>
        WeatherExtension.Update();

}
