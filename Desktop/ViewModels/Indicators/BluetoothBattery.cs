using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Desktop.Config;
using Desktop.Models;
using Desktop.ViewModels.Helpers;
using PostSharp.Patterns.Model;

namespace Desktop.ViewModels;

[NotifyPropertyChanged]
public class BluetoothBattery : TrackerIndicator
{

    public double? Value { get; private set; }

    public IEnumerable<BluetoothDevice> AvailableDevices { get; private set; } = Array.Empty<BluetoothDevice>();

    public Config.Trackers Config { get; } = ConfigManager.SystemInfo;

    public RelayCommand OpenAPIPageCommand { get; } = new(() => FileUtility.Open("https://www.bluetoothgoodies.com/info/battery-monitor-api/"));
    public RelayCommand OpenMainPageCommand { get; } = new(() => FileUtility.Open("https://www.bluetoothgoodies.com/"));

    public RelayCommand<MenuItem> PickDeviceCommand { get; }

    public BluetoothBattery()
    {

        PickDeviceCommand = new(menuItem =>
        {

            if (menuItem is null || menuItem.DataContext is not BluetoothDevice device || menuItem.Tag is not MenuItem parent)
                return;

            Config.SelectedDevice = device;

            var items = parent.Items.Cast<BluetoothDevice>().Select(d => parent.ItemContainerGenerator.ContainerFromItem(d)).OfType<MenuItem>().ToArray();
            foreach (var item in items)
                item.IsChecked = false;
            menuItem.IsChecked = true;

        });

        Config.PropertyChanged += (s, e) =>
        {
            IndicatorUtility.BluetoothDevices.RequestUpdate();
            Update();
        };

    }

    public override void Update()
    {

        Value = IndicatorUtility.BluetoothDevices.Value?.Find(Config.SelectedDevice?.Name)?.BatteryLevel;

        var devices = IndicatorUtility.BluetoothDevices.Value;
        if (!(devices?.SequenceEqual(AvailableDevices) ?? false))
            AvailableDevices = IndicatorUtility.BluetoothDevices.Value ?? Array.Empty<BluetoothDevice>();

    }

}
