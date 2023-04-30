using System.Windows.Controls.Primitives;
using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Views.Popups;

[NotifyPropertyChanged]
public partial class NotePopup : Popup
{

    public NotePopup() =>
        InitializeComponent();

    public Note? Note { get; private set; }

    public new bool IsOpen => base.IsOpen;

    public void Open(Note? note)
    {

        if (note is null)
            return;

        Note = note;
        base.IsOpen = true;

    }

}
