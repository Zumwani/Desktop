using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Config;
using Desktop.ViewModels.Helpers;
using Desktop.Views.Popups;

namespace Desktop.ViewModels;

public class Notes : ViewModel
{

    public ObservableCollection<Note> Items { get; } = new();

    public RelayCommand CreateCommand { get; }
    public RelayCommand<Models.Note> EditCommand { get; }

    readonly NotePopup popup = new();

    public bool HasItems => Items.Any();
    public Config.Notes Config { get; } = ConfigManager.Notes;

    public Notes()
    {

        CreateCommand = new(CreateNote);
        EditCommand = new(popup.Open);

        RefreshItems();
        Config.PropertyChanged += (s, e) =>
        {
            RefreshItems();
            OnPropertyChanged(nameof(HasItems));
        };

        Config.Items.OnAdded(_ => RefreshItems());
        Config.Items.OnRemoved(_ => RefreshItems());
        Config.Items.OnClear(Items.Clear);

    }

    void RefreshItems()
    {
        Items.Clear();
        foreach (var note in Config.Items)
            Add(note);
    }

    void Add(Models.Note note)
    {
        if (Config.ShowHiddenNotes || !note.IsHidden)
            Items.Add(new() { Model = note, EditCommand = EditCommand });
    }

    void CreateNote()
    {
        var note = new Models.Note() { Content = "New note" };
        Config.Items.Add(note);
        popup.Open(note);
    }

}
