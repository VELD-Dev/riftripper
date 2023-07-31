using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Drawing;

internal static class ViewMenuDraw
{
    internal static void ShowFramerateMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Show framerate", "", wnd.showFramerate, true))
            return;

        wnd.showFramerate = !wnd.showFramerate;
    }
}
