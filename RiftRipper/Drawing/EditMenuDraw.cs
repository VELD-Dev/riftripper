namespace RiftRipper.Drawing;

internal static class EditMenuDraw
{
    internal static void EditorSettingsMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Editor settings", "CTRL+SHIFT+E"))
            return;

        wnd.AddFrame(new EditorSettingsFrame(wnd));
        Console.WriteLine("Should open editor settings");
    }

    internal static void ProjectSettingsMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Project settings", "CTRL+SHIFT+P", false, wnd.openedProject != null))
            return;

        wnd.AddFrame(new ProjectSettingsFrame(wnd, wnd.openedProject));
        Console.WriteLine("Should open project settings");
    }

    internal static class ToolsMenuDraw
    {
        internal static void CreateLocalPortalMenuItem(Window wnd)
        {
            if (!ImGui.MenuItem("Create a local portal (R&C:RA only)", "P", false, wnd.openedGame == Games.RCRiftApart))
                return;

            Console.WriteLine("Opening portal creation modal !");
        }

        internal static void CreateDisplacementPortalMenuItem(Window wnd)
        {
            if (!ImGui.MenuItem("Create a displacement portal (R&C:RA only)", "SHIFT+P", false, wnd.openedGame == Games.RCRiftApart))
                return;

            Console.WriteLine("Opening displacement portal creation modal !");
        }

        internal static void CreateLevelPortalMenuItem(Window wnd)
        {
            if (!ImGui.MenuItem("Create a level portal (R&C:RA only)", "L", false, wnd.openedGame == Games.RCRiftApart))
                return;

            Console.WriteLine("Opening levels portal creation modal !");
        }

        internal static void CreatePocketRiftPortalMenuItem(Window wnd)
        {
            if (!ImGui.MenuItem("Create a pocket rift portal (R&C:RA only)", "SHIFT+L", false, wnd.openedGame == Games.RCRiftApart))
                return;

            Console.WriteLine("Opening pocket rift portal creation modal !");
        }
    }
}
