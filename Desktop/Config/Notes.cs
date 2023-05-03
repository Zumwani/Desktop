using System.Collections.ObjectModel;
using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Notes : ConfigModule
{
    public ObservableCollection<Note> Items { get; set; } = new();
    public bool ShowHiddenNotes { get; set; } = false;
}
