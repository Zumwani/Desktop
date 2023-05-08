using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Desktop.Models;
using PostSharp.Patterns.Model;

namespace Desktop.Config;

[NotifyPropertyChanged]
public class Notes : ConfigModule
{

    [AggregateAllChanges]
    public ObservableCollection<Note> Items { get; set; } = new();
    public bool ShowHiddenNotes { get; set; } = false;

    public void CreateNote() =>
        Items.Add(new());

    public Notes()
    {

        foreach (var item in Items)
            ((INotifyPropertyChanged)item).PropertyChanged += Notes_PropertyChanged;

        Items.OnAdded(item => ((INotifyPropertyChanged)item).PropertyChanged += Notes_PropertyChanged);
        Items.OnRemoved(item => ((INotifyPropertyChanged)item).PropertyChanged -= Notes_PropertyChanged);
        Items.OnClear(() => throw new NotSupportedException());

    }

    void Notes_PropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        NotifyPropertyChangedServices.SignalPropertyChanged(this, nameof(Items));

}
