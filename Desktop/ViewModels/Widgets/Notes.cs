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
    public RelayCommand ShowAllCommand { get; } = new(() => ConfigManager.Notes.ShowHiddenNotes = true);
    public RelayCommand HideAllCommand { get; } = new(() => ConfigManager.Notes.ShowHiddenNotes = false);

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
        Config.Items.OnRemoved(n => Items.RemoveWhere(i => i.Model == n));
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
            Items.Add(new() { Model = note, EditCommand = EditCommand, ShowAllCommand = ShowAllCommand, HideAllCommand = HideAllCommand });
    }

    void CreateNote()
    {
        var note = new Models.Note() { Content = "New note" };
        Config.Items.Add(note);
        popup.Open(note);
    }

}
