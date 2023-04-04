using System.Windows.Media;

namespace Desktop.Models;

public struct FileItem
{

    public string Title { get; set; }
    public string Path { get; set; }
    public ImageSource? Icon { get; set; }

}
