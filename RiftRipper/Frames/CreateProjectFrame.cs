using System.Text.RegularExpressions;
using FileDialog = RiftRipper.Utility.FileDialog;

namespace RiftRipper.Frames;

public class CreateProjectFrame : Frame
{
    protected override string frameName { get; set; } = "Create a project";

    public string projectName = "";
    public string projectPath = "C:\\";
    public string projectFileName = "";
    public string projectDescription = "Let's rip it out!";

    public CreateProjectFrame(Window wnd) : base(wnd)
    {
        
    }


    private bool HasCustomisedPath = false;
    public override void Render(float deltaTime)
    {
        unsafe 
        {
            int ProjectNameFilter(ImGuiInputTextCallbackData* data)
            {
                if (data->EventChar < 256 && "\\/:*?\"<>|".strchr(((char)data->EventChar)) == null)
                    return 0;
                return 1;
            }

            int  PathFilter(ImGuiInputTextCallbackData* data)
            {
                if (data->EventChar < 256 && "*?\"<>|".strchr((char)data->EventChar) == null)
                    return 0;
                return 1;
            }

            ImGui.InputTextWithHint("Project name", "Enter project name...", ref projectName, 64, ImGuiInputTextFlags.CallbackCharFilter, ProjectNameFilter);


            ImGui.BeginGroup();
                if (ImGui.InputTextWithHint("Path", "Path to project folder...", ref projectPath, 256, ImGuiInputTextFlags.CallbackCharFilter, PathFilter))
                    HasCustomisedPath = true;
                ImGui.SameLine();
                if (ImGui.Button("Choose"))
                {
                    HasCustomisedPath = true;
                    this.projectPath = FileDialog.OpenFolder("Choose project folder");
                }
            ImGui.EndGroup();
        }

        if (HasCustomisedPath is false)
        {
            this.projectPath = $"C:\\{projectName}";
        }

        if (ImGui.InputTextWithHint("Filename", "Name of the project file...", ref projectFileName, 32, ImGuiInputTextFlags.EnterReturnsTrue & ImGuiInputTextFlags.AutoSelectAll))
            projectFileName = projectFileName.Trim().EndsWith(".rift") ? projectFileName.Trim() : $"{projectFileName.Trim()}.rift";

        ImGui.InputTextMultiline("Description", ref projectDescription, 1024, new System.Numerics.Vector2(520, 300));

        ImGui.BeginGroup();
            if (ImGui.Button("Cancel")) window.Close();
            ImGui.SameLine();
            if (ImGui.Button("Proceed"))
            {
                
            }
        ImGui.EndGroup();
    }

    public override void RenderAsWindow(float deltaTime, ImGuiWindowFlags windowFlag = ImGuiWindowFlags.None)
    {
        ImGui.SetNextWindowSize(new System.Numerics.Vector2(800, 600));
        ImGui.SetNextWindowSizeConstraints(new System.Numerics.Vector2(16, 16), new System.Numerics.Vector2(1280, 720));
        base.RenderAsWindow(deltaTime, ImGuiWindowFlags.NoResize);
    }
}
