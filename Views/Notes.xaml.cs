using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Desktop.Models;

namespace Desktop.Views;

public partial class Notes : UserControl
{

    public Notes()
    {
        InitializeComponent();

    }

    void Note_Loaded(object sender, RoutedEventArgs e)
    {

        if (sender is FrameworkElement element)
        {
            elements.Add(element);
            ReloadVisibility(element);
        }

    }

    void Note_Unloaded(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element)
            _ = elements.Remove(element);
    }

    void ContextMenu_Closed(object sender, RoutedEventArgs e) =>
        ReloadVisibility();

    readonly List<FrameworkElement> elements = new();
    void ReloadVisibility()
    {
        foreach (var element in elements)
            ReloadVisibility(element);
    }

    static void ReloadVisibility(FrameworkElement element)
    {
        if (element.DataContext is Note note)
            element.Visibility = !note.IsHidden || Settings.ShowHiddenNotes.Current.Value ? Visibility.Visible : Visibility.Collapsed;
    }

    void Note_Clicked(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is Note note)
            NotePopup.Open(note);
    }

    void CreateNote_Click(object sender, RoutedEventArgs e)
    {
        var note = new Note() { Content = "New note" };
        Settings.Notes.Current.Add(note);
        NotePopup.Open(note);
    }
}
