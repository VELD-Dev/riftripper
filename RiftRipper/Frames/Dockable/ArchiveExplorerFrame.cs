using RiftRipperLib.DAT1.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulkan.Win32;
using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames.Dockable;

internal class ArchiveExplorerFrame : DockableFrame 
{
    protected override string frameName { get; set; } = "Archive explorer";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.None;
    protected override Vector2 default_position { get; set; } = ImGui.GetMainViewport().GetCenter();
    protected override ImGuiCond docking_condition { get; set; } = ImGuiCond.FirstUseEver;

    internal List<List<string>> Paths;
    internal List<string> currentPath = new() { "" };
    internal string currentPathString;

    public ArchiveExplorerFrame(Window wnd) : base(wnd)
    {
        if (!wnd.DAT1Manager.TOC.TryGetSection(out ArchiveFileSection archiveSection))
            return;

        foreach (var path in archiveSection.paths)
            Paths.Add(path.Split('\\').ToList());
    }

    public override void Render(float deltaTime)
    {
        currentPathString = Path.Combine(currentPath.ToArray()) ?? "";

        ImGui.BeginGroup();
        ImGui.Text("Path:");
        ImGui.SameLine();
        ImGui.InputText("Path", ref currentPathString, 0xFF);
        ImGui.EndGroup();

        ImGui.BeginGroup();
        var displayedFiles = new List<string>();
        var displayedDirs = new List<string>();
        if(currentPathString == "")
        {
            displayedFiles = Directory.GetFiles(Path.Combine(window.openedGamePath, "d")).ToList();
            displayedDirs = Directory.GetDirectories(Path.Combine(window.openedGamePath, "d")).ToList();
        }
        else
        {

            var allChildrenPath = Paths.Where(p =>
            {
                if (p.Count < currentPath.Count)
                    return false;

                var filter1 = p.FindAll(fold => p.IndexOf(fold) == currentPath.IndexOf(fold));
                if (filter1.Count < 1)
                    return false;

                return true;
            });

            foreach (var path in allChildrenPath)
            {
                Console.WriteLine($"Archive path: {Path.Combine(path.ToArray())}");
            }
        }
        ImGui.EndGroup();
    }

    public override void RenderAsWindow(float deltaTime)
    {
        base.RenderAsWindow(deltaTime);
    }
}
