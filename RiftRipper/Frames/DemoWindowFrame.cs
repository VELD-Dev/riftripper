using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Frames;

public class DemoWindowFrame : Frame
{
    protected override string frameName { get; set; } = "Demo Window Frame";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.None;
    public DemoWindowFrame(Window wnd) : base(wnd) { }

    public override void Render(float deltaTime)
    {}

    public override void RenderAsWindow(float deltaTime)
    {
        ImGui.ShowDemoWindow(ref isOpen);
    }
}
