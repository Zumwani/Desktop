using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Desktop.Utility;

public static class ConfigUtility
{

    static ConfigUtility() =>
        App.Current.Exit += (s, e) => DoSave();

    #region Queue

    static readonly Dictionary<string, Action> queue = new();
    static DispatcherTimer? timer;

    static void Queue(string key, Action action)
    {

        //We want to write latest value, so we need to discard previous request
        _ = queue.Remove(key);
        queue.Add(key, action);

        timer ??= new DispatcherTimer(TimeSpan.FromSeconds(0.5), DispatcherPriority.Background, (s, e) => DoSave(), App.Current.Dispatcher);
        timer.Stop();
        timer.Start();

    }

    #endregion
    #region Get

    public static T Get<T, TTarget>(this TTarget target, T defaultValue, [CallerMemberName] string propertyName = "") =>
        Get<T, TTarget>(target, propertyName) ?? defaultValue;

    public static T? Get<T, TTarget>(this TTarget target, [CallerMemberName] string propertyName = "")
    {

        if (target is null)
            throw new ArgumentNullException(nameof(target));

        return
            GetInternal(target, typeof(T), null, out var value, propertyName)
            ? (T)value
            : default;

    }

    static bool GetInternal(object target, Type type, object? defaultValue, [NotNullWhen(true)] out object? value, string propertyName = "")
    {

        value = null;
        if (target is null)
            throw new ArgumentNullException(nameof(target));

        var valueName = $"{target.GetType().FullName}.{propertyName}";

        using var key = Registry.CurrentUser.OpenSubKey(KeyPath);
        if (key is null)
            return false;

        var json = (string)key.GetValue(valueName)!;
        if (string.IsNullOrEmpty(json))
            return false;

        try
        {
            value = JsonSerializer.Deserialize(json, type) ?? defaultValue;
            return value is not null;
        }
        catch (JsonException)
        {
            return false;
        }

    }

    #endregion
    #region Set

    public static void Set<T, TTarget>(this TTarget target, T value, [CallerMemberName] string propertyName = "")
    {

        if (target is null)
            throw new ArgumentNullException(nameof(target));

        var valueName = $"{target.GetType().FullName}.{propertyName}";
        Queue(valueName, () =>
        {
            using var key = Registry.CurrentUser.CreateSubKey(KeyPath);
            var json = JsonSerializer.Serialize(value);
            key.SetValue(valueName, json);
        });

    }

    static void SetInternal(object target, Type type, object? value, string propertyName = "")
    {

        if (value is null || value.GetType() != type)
            throw new ArgumentException("Type mismatch", nameof(value));

        var method = typeof(ConfigUtility).GetMethods().First(m => m.Name == nameof(Set) && m.GetParameters().Length == 3);
        method = method.MakeGenericMethod(type, target.GetType());
        _ = method.Invoke(null, new[] { target, value, propertyName });

    }

    #endregion
    #region Setup object

    public static void Setup<T>(T obj) where T : INotifyPropertyChanged
    {

        var properties = obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(IsValidProperty);
        foreach (var property in properties)
            if (GetInternal(obj, property.PropertyType, property.GetValue(obj), out var value, property.Name))
                property.SetValue(obj, value);

        obj.PropertyChanged += (s, e) =>
        {

            var property = properties.FirstOrDefault(p => p.Name == e.PropertyName);
            if (property is not null)
                SetInternal(obj, property.PropertyType, property.GetValue(obj), property.Name);

        };

    }

    static bool IsValidProperty(PropertyInfo property)
    {

        if (property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
            return false;

        if (property.SetMethod is null)
            return false;

        if (property.SetMethod.IsPrivate && property.GetCustomAttribute<JsonIncludeAttribute>() is null)
            return false;

        return true;

    }

    #endregion

    static string KeyPath { get; } = GetKeyPath();

    static string GetKeyPath()
    {

        var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Could not find entry assembly.");
        var title = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? throw new InvalidOperationException("Could not find AssemblyTitleAttribute.");
        var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? throw new InvalidOperationException("Could not find AssemblyCompanyAttribute.");

        return $@"Software\{title}\{company}";

    }

    public static void DoSave()
    {

        timer?.Stop();

        foreach (var callback in queue)
            callback.Value.Invoke();
        queue.Clear();
        //Debug.WriteLine("saved");

    }

}
