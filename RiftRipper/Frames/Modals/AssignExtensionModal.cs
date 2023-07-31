#if WIN
using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames.Modals;

public class AssignExtensionModal : Modal
{
    protected override string frameName { get; set; } = "Assign ?";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.Modal | ImGuiWindowFlags.NoMove;

    public AssignExtensionModal(Window wnd) : base(wnd)
    {
    }

    public override void Render(float deltaTime)
    {
        ImGui.Text("Do you want to assign the .rift extension to RiftRipper ?\nIf yes, all the .rift files will be considered as RiftRipper projects.\n\nDo you want to proceed ?");
        ImGui.Separator();

        if (ImGui.Button("Yes", new Vector2(120, 0)))
        {
            ExtensionManager.AssociateExtension(".rift", "RiftRipper Project", Program.AppName);
            window.Settings.AskExtensionAssignation = false;
            ImGui.CloseCurrentPopup();
        }
        ImGui.SameLine();
        if (ImGui.Button("No, don't ask next time."))
        {
            window.Settings.AskExtensionAssignation = false;
            ImGui.CloseCurrentPopup();
        }
        ImGui.SameLine();
        if (ImGui.Button("No"))
        {
            ImGui.CloseCurrentPopup();
        }
    }

    public override void RenderAsWindow(float deltaTime)
    {
        base.RenderAsWindow(deltaTime);
    }
}
#endif