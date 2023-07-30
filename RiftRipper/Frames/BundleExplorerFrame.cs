using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Frames;

public class BundleExplorerFrame : Frame
{
    protected override string frameName { get; set; } = "Game files explorer";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.None;

    public BundleExplorerFrame(Window wnd) : base(wnd) { }

    public override void Render(float deltaTime)
    {
        ImGui.Text("Explore the game files. Double click on folders to open those, double click on level files to open them (dependencies will automatically be extracted too), right click + extract to extract a file.");
        
    }

    public override void RenderAsWindow(float deltaTime)
    {
        ImGui.SetNextWindowSize(new System.Numerics.Vector2(500, 800));
        base.RenderAsWindow(deltaTime);
    }
}
