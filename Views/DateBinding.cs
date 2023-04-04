using System;
using System.Windows.Data;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

public class DateExtension : Binding
{

    [NotifyPropertyChanged]
    public class Helper
    {
        public DateTime DateTime { get; private set; }
        public Helper() => ActionUtility.Invoke(TimeSpan.FromMilliseconds(10), () => DateTime = DateTime.Now);
    }

    static readonly Helper helper = new();

    public DateExtension()
    {
        Source = helper;
        Path = new(nameof(Helper.DateTime));
        Mode = BindingMode.OneWay;
    }

    public DateExtension(string path)
    {
        Source = helper;
        Path = new(nameof(Helper.DateTime) + "." + path);
        Mode = BindingMode.OneWay;
    }

}
