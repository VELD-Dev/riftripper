using Microsoft.Win32;

namespace RiftRipper;

internal class Program
{
    public const string AppName = "RiftRipper_Editor";
    public const string AppDisplayName = "RiftRipper";
    public static Version version = new(0, 0, 2);
    public static string ProvidedPath;
    public static string ProjectOpenPath;  // Console argument, will not be used for now.

#if WIN
    public static bool isExtAssociated = ExtensionManager.IsAssociated(".rift", AppName);
#endif

    [STAThread]
    static void Main(string[] args)
    {
        Console.Title = AppName;
#if WIN
        Console.WriteLine($"Is extension associated ? {isExtAssociated} - {ExtensionManager.IsAssociated(".rift", AppName)}");
#endif
        Window window = new(args);
        window.Run();
    }
}