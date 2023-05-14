using System.Windows;
using System.Windows.Controls;
using Desktop.Models;
using Duration = Desktop.Models.Duration;

namespace Desktop.Controls;

public partial class TimePicker : UserControl
{

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register("SelectedValue", typeof(double), typeof(TimePicker), new PropertyMetadata(0.0, OnSelectedValueChanged));

    public static readonly DependencyProperty SelectedUnitProperty =
        DependencyProperty.Register("SelectedUnit", typeof(TimeUnit), typeof(TimePicker), new PropertyMetadata(TimeUnit.Seconds, OnSelectedUnitChanged));

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(Duration), typeof(TimePicker), new PropertyMetadata(default(Duration), OnValueChanged));

    public double SelectedValue
    {
        get => (double)GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    public TimeUnit SelectedUnit
    {
        get => (TimeUnit)GetValue(SelectedUnitProperty);
        set => SetValue(SelectedUnitProperty, value);
    }

    public Duration Value
    {
        get => (Duration)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    static void OnSelectedValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
        ((TimePicker)sender).SetValue(value: (double)e.NewValue);

    static void OnSelectedUnitChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
        ((TimePicker)sender).SetValue(unit: (TimeUnit)e.NewValue);

    static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) =>
        ((TimePicker)sender).SetValue(duration: (Duration)e.NewValue);

    bool isSettingValue;
    void SetValue(double? value = null, TimeUnit? unit = null, Duration? duration = null)
    {

        if (isSettingValue)
            return;
        isSettingValue = true;

        if (duration.HasValue)
            SetValue(duration.Value);
        else
            SetValue(new() { Value = value ?? SelectedValue, Unit = unit ?? SelectedUnit });

        isSettingValue = false;

        void SetValue(Duration duration)
        {
            Value = duration;
            SelectedValue = duration.Value;
            SelectedUnit = duration.Unit;
        }

    }

    public TimePicker() =>
        InitializeComponent();

}
