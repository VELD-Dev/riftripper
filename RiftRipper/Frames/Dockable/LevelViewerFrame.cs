using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames.Dockable
{
    internal class LevelViewerFrame : DockableFrame
    {
        protected override string frameName { get; set; } = "Level 3D View";
        protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize;
        protected override Vector2 default_position { get; set; } = ImGui.GetMainViewport().GetWorkCenter();
        protected override ImGuiCond docking_condition { get; set; }

        internal LevelViewerFrame(Window wnd) : base(wnd) {}

        public override void Render(float deltaTime)
        {

        }

        public override void RenderAsWindow(float deltaTime)
        {
            base.RenderAsWindow(deltaTime);
        }
    }
}
