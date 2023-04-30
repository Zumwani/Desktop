using System.Diagnostics;
using System.IO;
using Common.Utility;
using Desktop.Models;

namespace Desktop.Commands;

public class OpenFile : Command<FileItem?>
{

    public override void Execute(FileItem? file)
    {
        if (File.Exists(file?.Path) || Directory.Exists(file?.Path))
            _ = Process.Start("explorer", file.Value.Path);
    }

}
