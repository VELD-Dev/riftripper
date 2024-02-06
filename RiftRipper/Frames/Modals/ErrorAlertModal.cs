using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames.Modals;

internal class ErrorAlertModal : Modal
{
    protected override string frameName { get; set; } = "An error occurred";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.Modal
        | ImGuiWindowFlags.AlwaysAutoResize
        | ImGuiWindowFlags.NoDocking
        | ImGuiWindowFlags.NoResize
        | ImGuiWindowFlags.NoCollapse;

    public readonly string ErrorMessage;
    private bool isTextWrapped = false;

    public ErrorAlertModal(Window wnd, string message, bool textWrapped = true) : base(wnd)
    {
        ErrorMessage = message;
        isTextWrapped = textWrapped;
    }


    public override void Render(float deltaTime)
    {
        if (isTextWrapped)
            ImGui.TextWrapped(ErrorMessage);
        else
            ImGui.Text(ErrorMessage);
        ImGui.Spacing();

        ImGui.Text("You can report this error to veld.dev on Discord, or create a new issue.\nBe careful, do not make duplicates.");
        ImGui.Spacing();

        if (ImGui.Button("OK"))
            isOpen = !isOpen;
        ImGui.SameLine();
#if WIN
        if(ImGui.Button("Copy error & Report"))
        {
            Clipboard.SetText(ErrorMessage);
            Process.Start(new ProcessStartInfo("https://github.com/VELD-Dev/riftripper/issues/new") { UseShellExecute = true });
        }
#else
        if(ImGui.Button("Report bug"))
        {
            Process.Start(new ProcessStartInfo("https://github.com/VELD-Dev/riftripper/issues/new") { UseShellExecute = true });
        }
#endif
    }

    public override void RenderAsWindow(float deltaTime)
    {
        //ImGui.SetNextWindowSize(new Vector2(400, 200));
        base.RenderAsWindow(deltaTime);
    }
}
