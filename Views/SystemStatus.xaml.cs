using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Desktop.Utility;
using PostSharp.Patterns.Model;
using Server.Models;

namespace Desktop.Views;

[NotifyPropertyChanged]
public partial class SystemStatus : UserControl
{

    public SystemInfo? SystemStats { get; private set; }
    public double? BluetoothDeviceBattery { get; private set; }
    public bool IsIdle { get; set; }

    public SystemStatus()
    {
        ActionUtility.Invoke(TimeSpan.FromSeconds(2), Reload);
        InitializeComponent();
    }

    async void Reload()
    {
        SystemStats = await Server.API.GetSystemStatus();
        var bluetoothDevice = await BluetoothUtility.Get(Settings.BluetoothDevice.Current.Value);
        BluetoothDeviceBattery = bluetoothDevice?.IsConnected ?? false ? bluetoothDevice.Value.Level : null;
    }

    #region Bluetooth device list

    void MenuItem_Click(object sender, RoutedEventArgs e)
    {

        if (sender is MenuItem item)
            Settings.BluetoothDevice.Current.Value = ((BluetoothUtility.Device)item.DataContext).Name;

        ReloadBluetoothDeviceList();
        Reload();

    }

    void ContextMenu_Opened(object sender, RoutedEventArgs e) =>
        ReloadBluetoothDeviceList();

    void ReloadBluetoothDeviceList()
    {
        foreach (var item in BluetoothDeviceButton.ContextMenu.ItemContainerGenerator.Items.Select(BluetoothDeviceButton.ContextMenu.ItemContainerGenerator.ContainerFromItem).OfType<System.Windows.Controls.MenuItem>().ToArray())
            item.IsChecked = Settings.BluetoothDevice.Current.Value == ((BluetoothUtility.Device)item.DataContext).Name;
    }

    #endregion

}

public class BluetoothDevicesExtension : MarkupExtension
{

    public override object ProvideValue(IServiceProvider serviceProvider)
    {

        var list = new ObservableCollection<BluetoothUtility.Device>();

        _ = BluetoothUtility.Get().ContinueWith(t =>
            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in t.Result)
                    list.Add(item);
            }));

        return list;

    }

}
