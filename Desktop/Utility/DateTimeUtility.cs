using System;
using Microsoft.Win32;

namespace Desktop.ViewModels.Helpers;

public static class DateTimeUtility
{

    public static string SystemDateFormat { get; private set; } = null!;
    public static string SystemTimeFormat { get; private set; } = null!;

    public static event Action? OnFormatsChanged;

    static DateTimeUtility()
    {

        Reload();

        SystemEvents.UserPreferenceChanged += (s, e) =>
        {
            if (e.Category is UserPreferenceCategory.Locale)
                Reload();
        };

    }

    static void Reload()
    {

        //wtf microsoft, why do you not expose this in the framework?
        using var key =
            Registry.CurrentUser.OpenSubKey(@"Control Panel\International")
            ?? throw new InvalidOperationException();

        SystemDateFormat = (string?)key.GetValue("sLongDate") ?? throw new InvalidOperationException();
        SystemTimeFormat = (string?)key.GetValue("sShortTime") ?? throw new InvalidOperationException();

        OnFormatsChanged?.Invoke();

    }

}