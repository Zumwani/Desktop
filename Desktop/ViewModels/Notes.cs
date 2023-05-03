using System.Linq;
using System.Windows.Data;
using Desktop.Config;
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

    public bool HasItems => Items.View.Cast<object>().Count() > 0;
    public Config.Notes Config { get; } = ConfigManager.Notes;

    public Notes()
    {

        ClickCommand = new(popup.Open);
        CreateCommand = new(CreateNote);

        Items = new CollectionViewSource { Source = Config.Items };
        Items.Filter += (s, e) => e.Accepted = Config.ShowHiddenNotes || !((Note)e.Item).IsHidden;
        Config.PropertyChanged += (s, e) =>
        {
            RefreshItems();
            OnPropertyChanged(nameof(HasItems));
        };

    }

    void RefreshItems() =>
        Items.View.Refresh();

    void CreateNote()
    {
        var note = new Note() { Content = "New note" };
        Config.Items.Add(note);
        popup.Open(note);
    }

}
