using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.ViewModels.Helpers;

public static class MenuItemUtility
{

    public static object GetSelectedItem(MenuItem obj) => obj.GetValue(SelectedItemProperty);
    public static void SetSelectedItem(MenuItem obj, object value) => obj.SetValue(SelectedItemProperty, value);

    //public static object GetSelectedItem(ContextMenu obj) => obj.GetValue(SelectedItemProperty);
    //public static void SetSelectedItem(ContextMenu obj, object value) => obj.SetValue(SelectedItemProperty, value);

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(MenuItemUtility), new PropertyMetadata(null, OnSelectedItemChanged));

    static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {

        if (sender is ContextMenu menu)
            Setup(menu, e);
        else if (sender is MenuItem item)
            Setup(item, e);

    }

    static void Setup(ContextMenu item, DependencyPropertyChangedEventArgs e)
    {

        item.Opened -= Item_SubmenuOpened;
        item.Opened += Item_SubmenuOpened;

        void Item_SubmenuOpened(object sender, RoutedEventArgs e) =>
            UpdateCheckedState();

        async void UpdateCheckedState()
        {

            while (item.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                await Task.Delay(100);

            var children = item.Items.Cast<object>().
                Select(d => item.ItemContainerGenerator.ContainerFromItem(d)).
                OfType<MenuItem>().
                Where(m => m.IsCheckable).
                ToArray();

            foreach (var child in children)
                child.IsChecked = Equals(child.DataContext, e.NewValue);

        }

    }

    static void Setup(MenuItem item, DependencyPropertyChangedEventArgs e)
    {

        item.SubmenuOpened -= Item_SubmenuOpened;
        item.SubmenuOpened += Item_SubmenuOpened;

        void Item_SubmenuOpened(object sender, RoutedEventArgs e) =>
            UpdateCheckedState();

        async void UpdateCheckedState()
        {

            while (item.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                await Task.Delay(100);

            var children = item.Items.Cast<object>().
                Select(d => item.ItemContainerGenerator.ContainerFromItem(d)).
                OfType<MenuItem>().
                Where(m => m.IsCheckable).
                ToArray();

            foreach (var child in children)
                child.IsChecked = Equals(child.DataContext, e.NewValue);

        }

    }

}
