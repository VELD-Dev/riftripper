using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames.Dockable;

public abstract class DockableFrame : Frame
{
    protected abstract ImGuiCond docking_condition { get; set; }
    protected abstract Vector2 default_position { get; set; }

    public DockableFrame(Window wnd) : base(wnd)
    {}

    public override void RenderAsWindow(float deltaTime)
    {
        uint dockspaceId = ImGui.GetID("dockspace");
        ImGui.SetNextWindowDockID(dockspaceId, docking_condition);
        ImGui.SetNextWindowPos(default_position, docking_condition);
        base.RenderAsWindow(deltaTime);
    }
}
