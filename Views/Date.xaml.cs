using System.Windows.Controls;
using PostSharp.Patterns.Model;

namespace Desktop.Views;

[NotifyPropertyChanged]
public partial class Date : UserControl
{

    public bool IsIdle { get; set; }

    public Date() =>
        InitializeComponent();

}
