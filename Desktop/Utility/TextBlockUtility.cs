using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.ViewModels.Helpers;

public static class TextBlockUtility
{

    public static bool GetSelectAllOnTripleClick(TextBox obj) => (bool)obj.GetValue(SelectAllOnTripleClickProperty);
    public static void SetSelectAllOnTripleClick(TextBox obj, bool value) => obj.SetValue(SelectAllOnTripleClickProperty, value);

    public static readonly DependencyProperty SelectAllOnTripleClickProperty =
        DependencyProperty.RegisterAttached("SelectAllOnTripleClick", typeof(bool), typeof(TextBlockUtility), new PropertyMetadata(false, OnSelectAllOnTripleClickChanged));

    static void OnSelectAllOnTripleClickChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {

        if (sender is not TextBox textBox)
            return;

        textBox.PreviewMouseLeftButtonDown += TextBlock_MouseLeftButtonDown;

        void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3)
                textBox.SelectAll();
        }

    }

}