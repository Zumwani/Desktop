using System.Collections.ObjectModel;
using System.Linq;
using Desktop.Config;
using Desktop.ViewModels.Helpers;

namespace Desktop.ViewModels;

public class Notes : ViewModel
{

    public ObservableCollection<Note> Items { get; } = new();

    public RelayCommand CreateCommand { get; }
    public RelayCommand ShowAllCommand { get; } = new(() => ConfigManager.Notes.ShowHiddenNotes = true);
    public RelayCommand HideAllCommand { get; } = new(() => ConfigManager.Notes.ShowHiddenNotes = false);

    public bool HasItems => Config.ShowHiddenNotes ? Items.Any() : Items.Any(i => i.IsVisible);
    public Config.Notes Config { get; } = ConfigManager.Notes;

    public Notes()
    {

        CreateCommand = new(Config.CreateNote);

        RefreshItems();
        Config.PropertyChanged += (s, e) => OnPropertyChanged(nameof(HasItems));

        Config.Items.OnAdded(Add);
        Config.Items.OnRemoved(n => Items.RemoveWhere(i => i.Model == n));
        Config.Items.OnClear(Items.Clear);

        Config.Items.OnChanged(() => OnPropertyChanged(nameof(HasItems)));

    }

    void RefreshItems()
    {
        Items.Clear();
        foreach (var note in Config.Items)
            Add(note);
    }

    void Add(Models.Note note)
    {
        var item = new Note() { Model = note, ShowAllCommand = ShowAllCommand, HideAllCommand = HideAllCommand };
        Items.Add(item);
    }

}
