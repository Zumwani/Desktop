using System.Windows.Controls;

namespace Desktop.Views;

public partial class Weather : UserControl
{

    public bool IsIdle { get; set; }

    public Weather() =>
        InitializeComponent();

}
