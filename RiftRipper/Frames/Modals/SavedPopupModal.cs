namespace RiftRipper.Frames.Modals;

internal class SavedPopupModal : Modal
{
    protected override string frameName { get; set; }
    protected override ImGuiWindowFlags window_flags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string message;

    public SavedPopupModal(Window wnd, string msg, string caller) : base(wnd)
    {
        frameName = caller;
        message = msg;
    }

    public override void Render(float deltaTime)
    {
        ImGui.Text(message);
        ImGui.Spacing();
        if (ImGui.Button("OK"))
        {
            isOpen = !isOpen;
        }
    }
}
