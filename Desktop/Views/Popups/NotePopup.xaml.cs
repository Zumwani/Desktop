using System.Windows;
using System.Windows.Controls.Primitives;
using Desktop.Models;

namespace Desktop.Views.Popups;

public partial class NotePopup : Popup
{

    public NotePopup() =>
        InitializeComponent();

    public Note Note
    {
        get => (Note)GetValue(NoteProperty);
        set => SetValue(NoteProperty, value);
    }

    public static readonly DependencyProperty NoteProperty =
        DependencyProperty.Register("Note", typeof(Note), typeof(NotePopup), new PropertyMetadata(default(Note)));

}
