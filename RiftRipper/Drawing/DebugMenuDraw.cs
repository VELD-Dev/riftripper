namespace RiftRipper.Drawing;

internal static class DebugMenuDraw
{
    internal static void Menu(Window wnd)
    {
        if (!wnd.Settings.DebugMode)
            return;

        if (ImGui.BeginMenu("Debug"))
        {
            DebugItems.DebugMenuItem(wnd);
            ImGui.EndMenu();
        }
    }

    internal static class DebugItems
    {
        internal static void DebugMenuItem(Window wnd)
        {
            if (!ImGui.MenuItem("Demo frame"))
                return;

            wnd.AddFrame(new DemoWindowFrame(wnd));
        }
    }
}