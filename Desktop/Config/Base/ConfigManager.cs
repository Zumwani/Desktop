using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace Desktop.Config;

public static class ConfigManager
{

    public static DateAndTime DateAndTime { get; } = new();
    public static Files Files { get; } = new();
    public static General General { get; } = new();
    public static IdleMode IdleMode { get; } = new();
    public static Notes Notes { get; } = new();
    public static Notifications Notifications { get; } = new();
    public static SystemInfo SystemInfo { get; } = new();
    public static Weather Weather { get; } = new();
    public static DesktopWindow DesktopWindow { get; } = new();

    static ConfigManager()
    {
        foreach (var property in typeof(ConfigManager).GetProperties())
            modules.Add(property.PropertyType, (ConfigModule)property.GetValue(null)!);
    }

    static readonly Dictionary<Type, ConfigModule> modules = new();

    public static T GetModule<T>() where T : ConfigModule =>
        (T)modules[typeof(T)];

}

public class NotesExtension : Binding
{

    public NotesExtension() : base() { Source = ConfigManager.Notes; }
    public NotesExtension(string propertyPath) : base(propertyPath) { Source = ConfigManager.Notes; }

}
