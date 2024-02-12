using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility;

public class Overlays
{
    public static void ShowOverlay(Window wnd, bool p_open)
    {
        int location = 0;
        ImGuiIOPtr io = ImGui.GetIO();
        ImGuiWindowFlags window_flags = ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoFocusOnAppearing | ImGuiWindowFlags.NoNav;
        if (location >= 0)
        {
            float PAD = 10.0f;
            ImGuiViewportPtr viewport = ImGui.GetMainViewport();
            System.Numerics.Vector2 work_pos = viewport.WorkPos; // Use work area to avoid menu-bar/task-bar, if any!
            System.Numerics.Vector2 work_size = viewport.WorkSize;
            System.Numerics.Vector2 window_pos, window_pos_pivot;
            window_pos.X = (location == 1) ? (work_pos.X + work_size.X - PAD) : (work_pos.X + PAD);
            window_pos.Y = (location == 2) ? (work_pos.Y + work_size.Y - PAD) : (work_pos.Y + PAD);
            window_pos_pivot.X = (location == 1) ? 1.0f : 0.0f;
            window_pos_pivot.Y = (location == 2) ? 1.0f : 0.0f;
            ImGui.SetNextWindowPos(window_pos, ImGuiCond.Always, window_pos_pivot);
            window_flags |= ImGuiWindowFlags.NoMove;
        }
        else if (location == -2)
        {
            // Center window
            ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Always, new System.Numerics.Vector2(0.5f, 0.5f));
            window_flags |= ImGuiWindowFlags.NoMove;
        }
        ImGui.SetNextWindowBgAlpha(0.35f); // Transparent background
        if (ImGui.Begin("Framerate overlay", ref p_open, window_flags))
        {
            ImGui.Text("Statistics");
            ImGui.Separator();
            ImGui.Text($"Framerate: {Math.Round(ImGui.GetIO().Framerate)}FPS");
            if (ImGui.BeginPopupContextWindow())
            {
                if (ImGui.MenuItem("Custom", "", location == -1)) location = -1;
                if (ImGui.MenuItem("Center", "", location == -2)) location = -2;
                if (ImGui.MenuItem("Top-left", "", location == 0)) location = 0;
                if (ImGui.MenuItem("Top-right", "", location == 1)) location = 1;
                if (ImGui.MenuItem("Bottom-left", "", location == 2)) location = 2;
                if (ImGui.MenuItem("Bottom-right", "", location == 3)) location = 3;
                if (p_open && ImGui.MenuItem("Close")) p_open = false;
                ImGui.EndPopup();
            }
        }
        ImGui.End();
    }
}
