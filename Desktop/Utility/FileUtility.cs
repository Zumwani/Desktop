using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common.Utility;
using Desktop.Models;

namespace Desktop.Utility;

public static class FileUtility
{

    public static ObservableCollection<FileItem> Files { get; } = new();

    static FileUtility()
    {
        Refresh();
        CreateWatcher(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        CreateWatcher(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
    }

    static void Add(string path)
    {
        if (!Files.Any(f => f.Path == path) && !path.EndsWith("desktop.ini"))
            Files.Add(Create(path));
    }

    static FileItem Create(string path) =>
        new() { Path = path, Title = Path.GetFileName(path), Icon = GetIcon(path) };

    public static void Open(string path, string args = "") =>
        Process.Start(new ProcessStartInfo(path) { UseShellExecute = true, Arguments = args });

    #region Watcher

    static void Refresh() =>
        App.Current.Dispatcher.Invoke(() =>
        {

            Files.Clear();
            foreach (var folder in Directory.EnumerateDirectories(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*")) Add(folder);
            foreach (var folder in Directory.EnumerateDirectories(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"), "*")) Add(folder);
            foreach (var file in Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*")) Add(file);
            foreach (var file in Directory.EnumerateFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"), "*")) Add(file);

        });

    static readonly Dictionary<string, FileSystemWatcher> watchers = new();
    static void CreateWatcher(string path)
    {

        if (watchers.TryGetValue(path, out var watcher) && watcher is not null)
        {
            watcher.Changed -= Watcher_Changed;
            watcher.Created -= Watcher_Changed;
            watcher.Deleted -= Watcher_Changed;
            watcher.Renamed -= Watcher_Changed;
            watcher.Dispose();
        }

        watcher = new() { Path = path, EnableRaisingEvents = true, IncludeSubdirectories = false };
        watchers.Set(path, watcher);
        watcher.Changed += Watcher_Changed;
        watcher.Created += Watcher_Changed;
        watcher.Deleted += Watcher_Changed;
        watcher.Renamed += Watcher_Changed;
        watcher.Error += Watcher_Error;

        async void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            await Task.Delay(100);
            Refresh();
        }

        void Watcher_Error(object sender, ErrorEventArgs e) =>
            CreateWatcher(path);

    }

    #endregion
    #region Icon

    public static ImageSource? GetIcon(string? strPath)
    {

        if (string.IsNullOrEmpty(strPath))
            return null;

        var info = new Interop.SHFILEINFO(true);
        var cbFileInfo = Marshal.SizeOf(info);

        var flags = Interop.SHGFI.Icon | Interop.SHGFI.SmallIcon | Interop.SHGFI.UseFileAttributes;
        var fileAttribute =
            Directory.Exists(strPath)
            ? Interop.FILE_ATTRIBUTE.FILE_ATTRIBUTE_DIRECTORY
            : Interop.FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;

        _ = Interop.SHGetFileInfo(strPath, fileAttribute, out info, (uint)cbFileInfo, flags);

        var iconHandle = info.hIcon;

        var img = Imaging.CreateBitmapSourceFromHIcon(
                    iconHandle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

        _ = Interop.DestroyIcon(iconHandle);

        return img;
    }

    static class Interop
    {

        /// <summary>Maximal Length of unmanaged Windows-Path-strings</summary>
        const int MAX_PATH = 260;
        /// <summary>Maximal Length of unmanaged Typename</summary>
        const int MAX_TYPE = 80;

        [Flags]
        public enum SHGFI : int
        {
            /// <summary>get icon</summary>
            Icon = 0x000000100,
            /// <summary>get display name</summary>
            DisplayName = 0x000000200,
            /// <summary>get type name</summary>
            TypeName = 0x000000400,
            /// <summary>get attributes</summary>
            Attributes = 0x000000800,
            /// <summary>get icon location</summary>
            IconLocation = 0x000001000,
            /// <summary>return exe type</summary>
            ExeType = 0x000002000,
            /// <summary>get system icon index</summary>
            SysIconIndex = 0x000004000,
            /// <summary>put a link overlay on icon</summary>
            LinkOverlay = 0x000008000,
            /// <summary>show icon in selected state</summary>
            Selected = 0x000010000,
            /// <summary>get only specified attributes</summary>
            Attr_Specified = 0x000020000,
            /// <summary>get large icon</summary>
            LargeIcon = 0x000000000,
            /// <summary>get small icon</summary>
            SmallIcon = 0x000000001,
            /// <summary>get open icon</summary>
            OpenIcon = 0x000000002,
            /// <summary>get shell size icon</summary>
            ShellIconSize = 0x000000004,
            /// <summary>pszPath is a pidl</summary>
            PIDL = 0x000000008,
            /// <summary>use passed dwFileAttribute</summary>
            UseFileAttributes = 0x000000010,
            /// <summary>apply the appropriate overlays</summary>
            AddOverlays = 0x000000020,
            /// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
            OverlayIndex = 0x000000040,
        }

        public enum FILE_ATTRIBUTE : int
        {
            FILE_ATTRIBUTE_NORMAL = 0x80,
            FILE_ATTRIBUTE_DIRECTORY = 0x10
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public SHFILEINFO(bool _)
            {
                hIcon = IntPtr.Zero;
                iIcon = 0;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_TYPE)]
            public string szTypeName;
        };

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetFileInfo(string pszPath, FILE_ATTRIBUTE dwFileAttributes, out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyIcon(IntPtr hIcon);

    }

    #endregion

}
