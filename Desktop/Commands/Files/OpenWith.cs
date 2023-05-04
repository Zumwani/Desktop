using System;
using System.Diagnostics;
using System.IO;
using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class OpenWith : Command<FileItem?>
{

    public override void Execute(FileItem? file)
    {
        var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
        args += ",OpenAs_RunDLL " + file?.Path;
        _ = Process.Start("rundll32.exe", args);
    }

}
