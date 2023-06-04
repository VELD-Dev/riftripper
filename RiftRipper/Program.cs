namespace RiftRipper;

internal class Program
{
    public static Version version = new(0, 1, 1);
    public static string ProvidedPath;
    [STAThread]
    static void Main(string[] args)
    {
        Window window = new(args);
        window.Run();
    }
}