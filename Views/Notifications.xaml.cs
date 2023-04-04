using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Desktop.Utility;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

[NotifyPropertyChanged]
public partial class Notifications : UserControl
{

    DesktopWindow? window;
    public bool IsIdle { get; set; }

    public Notifications() =>
        InitializeComponent();

    void UserControl_Loaded(object sender, RoutedEventArgs e) =>
        window = (DesktopWindow)Window.GetWindow(this);

}

public class NotificationsExtension : MarkupExtension
{

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        NotificationUtility.Notifications;

}
