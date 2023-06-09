﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Desktop.Utility;

public static class ObservableCollectionUtility
{

    public static void OnAdded<T>(this ObservableCollection<T> list, Action<T> callback)
    {
        list.CollectionChanged += (s, e) =>
        {
            if (e.NewItems?.Count > 0)
                foreach (T item in e.NewItems)
                    callback.Invoke(item);
        };
    }

    public static void OnRemoved<T>(this ObservableCollection<T> list, Action<T> callback)
    {
        list.CollectionChanged += (s, e) =>
        {
            if (e.OldItems?.Count > 0)
                foreach (T item in e.OldItems)
                    callback.Invoke(item);
        };
    }

    public static void OnClear<T>(this ObservableCollection<T> list, Action callback) =>
        list.CollectionChanged += (s, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
                callback.Invoke();
        };

    public static void OnChanged<T>(this ObservableCollection<T> list, Action callback) =>
        list.CollectionChanged += (s, e) =>
        {
            callback.Invoke();
        };

    public static void RemoveWhere<T>(this ObservableCollection<T> list, Func<T, bool> callback)
    {
        foreach (var item in list.Where(callback).ToArray())
            _ = list.Remove(item);
    }

}
