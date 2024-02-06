using RiftRipper.Frames.Modals;
using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames;

public class EditorSettingsFrame : Frame
{
    protected override string frameName { get; set; } = "Editor settings";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.NoResize;
    public EditorConfigs settings;

    public EditorSettingsFrame(Window wnd) : base(wnd)
    {
        settings = wnd.Settings;
    }

    public override void Render(float deltaTime)
    {
        ImGui.Text("Global settings");
        ImGui.BeginGroup();
        ImGui.Checkbox("Debug mode", ref settings.DebugMode);
        ImGui.DragFloat("Render distance", ref settings.RenderDistance, 0.25f, 25f, 10000f, "%.2f");
        ImGui.DragFloat("Camera speed", ref settings.MoveSpeed, 0.01f, 0.01f, 100f, "%.2f");
        ImGui.DragFloat("Camera shift speed", ref settings.MaxSpeed);
        ImGui.EndGroup();

        ImGui.SeparatorText("Advanced");

#if WIN
        ImGui.BeginGroup();
        if (ExtensionManager.IsAssociated(".rift", Program.AppName))
        {
            if (ImGui.Button("Dissociate .rift files from app"))
                ExtensionManager.TryDissociateExtension(".rift", Program.AppName);
        }
        else
        {
            if (ImGui.Button("Associate .rift files to the app"))
                ExtensionManager.AssociateExtension(".rift", "RiftRipper Project", Program.AppName);
        }
        ImGui.EndGroup();
#endif

        ImGui.Separator();

        ImGui.BeginGroup();
        if(ImGui.Button("Apply and save"))
        {
            settings.SaveSettingsToFile();
        }
        ImGui.SameLine();
        if(ImGui.Button("Cancel"))
        {
            settings.ReloadSettings();
        }
        ImGui.SameLine();
        if(ImGui.Button("Close"))
        {
            settings.ReloadSettings();
            isOpen = false;
        }
        ImGui.EndGroup();
    }

    public override void RenderAsWindow(float deltaTime)
    {
        ImGui.SetNextWindowSize(new Vector2(800, 600));
        base.RenderAsWindow(deltaTime);
    }
}
