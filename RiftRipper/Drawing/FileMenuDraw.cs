using FileDialog = RiftRipper.Utility.FileDialog;

namespace RiftRipper.Drawing;

internal static class FileMenuDraw
{
    internal static void OpenGameMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Open game files", "CTRL+G"))
            return;

        var res = FileDialog.OpenFile("Select game executable", ".exe");
        if (res.Length < 1)
            return;

        Console.WriteLine($"Folder found: {Path.GetDirectoryName(res)}, must check if it's a game.");
        switch (Path.GetFileName(res))
        {
            case "RiftApart.exe":  // Must match perfectly.
                Console.WriteLine($"Game is Ratchet & Clank: Rift Apart.");
                wnd.openedGame = Games.RCRiftApart;
                wnd.DAT1Manager = new Utility.Luna.DAT1Manager(Path.GetDirectoryName(res));
                break;
            default:
                Console.WriteLine("Game not supported.");
                return;
        }
        var dirPath = Path.GetDirectoryName(res);
        wnd.openedGamePath = dirPath;
        wnd.AddFrame(new ArchiveExplorerFrame(wnd));
    }

    internal static void CreateRiftProjectMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Create a Rift project", "CTRL+P"))
            return;

        wnd.AddFrame(new CreateProjectFrame(wnd));
        Console.WriteLine("Should open project creation menu");
    }

    internal static void OpenRiftProjectMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Open a Rift project (.rift)", "CTRL+ALT+P"))
            return;

        var res = FileDialog.OpenFile("Open project", ".rift");
        if (res.Length < 0)
            return;

        try
        {
            wnd.openedProject = Project.OpenFromFile(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    internal static void CloseActiveProjectMenuItem(Window wnd)
    {
        if (wnd.openedProject == null)
            return;

        if (!ImGui.MenuItem("Close the active project", "SHIFT+ALT+P"))
            return;

        wnd.openedProject = null;
    }

    internal static void CloseRiftripperMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Quit", "ALT+F4"))
            return;

        Environment.Exit(0);
    }
}
