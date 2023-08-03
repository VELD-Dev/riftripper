using RiftRipperLib.DAT1.Sections;
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

    internal static void ShowArchiveExplorer(Window wnd)
    {

        var frameAlreadyOpened = wnd.IsAnyFrameOpened<ArchiveExplorerFrame>();
        if (!ImGui.MenuItem("Archive explorer", "", frameAlreadyOpened, wnd.DAT1Manager != null))
            return;

        if(frameAlreadyOpened)
            wnd.TryCloseFirstFrame<ArchiveExplorerFrame>();
        else
            wnd.AddFrame(new ArchiveExplorerFrame(wnd));
    }
}
