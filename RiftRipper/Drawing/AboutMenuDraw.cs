using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Drawing;

internal static class AboutMenuDraw
{
    internal static void OfficialGithubRepoMenuItem(Window wnd)
    {
        if (!ImGui.MenuItem("Official GitHub repository"))
            return;

        Process.Start(new ProcessStartInfo("https://github.com/VELD-Dev/riftripper") { UseShellExecute = true });
    }
}
