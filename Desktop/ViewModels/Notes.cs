using System.ComponentModel;
using System.Windows.Data;
using Desktop.Models;
using Desktop.ViewModels.Helpers;
using Desktop.Views.Popups;

namespace Desktop.ViewModels;

public class Notes : ViewModel
{

    public CollectionViewSource Items { get; private set; }
    public RelayCommand<Note> ClickCommand { get; }
    public RelayCommand CreateCommand { get; }

    readonly NotePopup popup = new();

    public Notes()
    {

        ClickCommand = new(popup.Open);
        CreateCommand = new(CreateNote);

        Items = new CollectionViewSource { Source = Settings.Notes.Current };
        Items.Filter += (s, e) => e.Accepted = Settings.ShowHiddenNotes.Current || !((Note)e.Item).IsHidden;
        Settings.ShowHiddenNotes.Current.PropertyChanged += ShowHiddenNotesChanged;

    }

    void ShowHiddenNotesChanged(object? sender, PropertyChangedEventArgs e) =>
        RefreshItems();

    void RefreshItems() =>
        Items.View.Refresh();

    void CreateNote()
    {
        var note = new Note() { Content = "New note" };
        Settings.Notes.Current.Add(note);
        popup.Open(note);
    }

}
