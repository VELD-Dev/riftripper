using System.Reflection;
using System.Text.RegularExpressions;
using FileDialog = RiftRipper.Utility.FileDialog;

namespace RiftRipper.Frames;

public class CreateProjectFrame : Frame
{
    protected override string frameName { get; set; } = "Create a project";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.NoResize;

    public string projectId = "";
    public string projectName = "";
    public string projectAuthor = "";
    public string projectPath = "C:\\";
    public string projectFileName = "";
    public string projectVersion = "1.0.0";
    public string projectDescription = "Let's rip it out!";

    public CreateProjectFrame(Window wnd) : base(wnd) { }


    private bool HasCustomisedPath = false;
    private bool HasCustomisedFile = false;
    private bool HasCustomisedId = false;
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

            int ProjectAuthorFilter(ImGuiInputTextCallbackData* data)
            {
                if("\",`\\".strchr((char)data->EventChar) == null) return 0; return 1;
            }

            int ProjectIdentifierFilter(ImGuiInputTextCallbackData* data)
            {
                if (data->EventChar < 256 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-".strchr((char)data->EventChar) != null)
                    return 0;
                return 1;
            }

            int PathFilter(ImGuiInputTextCallbackData* data)
            {
                if (data->EventChar < 256 && "*?\"<>|".strchr((char)data->EventChar) == null)
                    return 0;
                return 1;
            }

            int VersionFilter(ImGuiInputTextCallbackData* data)
            {
                var charsToLower = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (charsToLower.strchr((char)data->EventChar) != null)
                    data->EventChar = char.ToLower((char)data->EventChar);

                if(data->EventChar < 256 && "abcdefghijklmnopqrstuvwxyz0123456789._-~+".strchr((char)data->EventChar) != null)
                {
                    return 0;
                }
                return 1;
            }

            ImGui.BeginGroup();
                ImGui.InputTextWithHint("Project name", "Enter project name...", ref projectName, 64, ImGuiInputTextFlags.CallbackCharFilter, ProjectNameFilter);
                ImGui.SameLine();
                ImGuiPlus.RequiredMarker();
            ImGui.EndGroup();

            ImGui.BeginGroup();
                ImGui.InputTextWithHint("Project author", "'V E L D', 'VELD-Dev'...", ref projectAuthor, 128, ImGuiInputTextFlags.CallbackCharFilter, ProjectAuthorFilter);
                ImGui.SameLine();
                ImGuiPlus.RequiredMarker();
            ImGui.EndGroup();

            ImGui.BeginGroup();
                if (ImGui.InputTextWithHint("Path", "Path to project folder...", ref projectPath, 256, ImGuiInputTextFlags.CallbackCharFilter, PathFilter))
                    HasCustomisedPath = true;
                ImGui.SameLine();
                if (ImGui.Button("Choose"))
                {
                    HasCustomisedPath = true;
                    this.projectPath = FileDialog.OpenFolder("Choose project folder") + "\\";
                }
                ImGui.SameLine();
                ImGuiPlus.RequiredMarker("The path must be a FOLDER, not a FILE.");
            ImGui.EndGroup();

            ImGui.BeginGroup();
                if (ImGui.InputTextWithHint("Filename", "Name of the project file...", ref projectFileName, 32, ImGuiInputTextFlags.EnterReturnsTrue & ImGuiInputTextFlags.AutoSelectAll, ProjectNameFilter))
                {
                    projectFileName = projectFileName.Trim().EndsWith(".rift") ? projectFileName.Trim() : $"{projectFileName.Trim()}.rift";
                    HasCustomisedFile = true;
                }
                ImGui.SameLine();
                ImGuiPlus.RequiredMarker();
            ImGui.EndGroup();

            ImGui.BeginGroup();
                if (ImGui.InputTextWithHint("Project identifier", "com.author.project_name", ref projectId, 32, ImGuiInputTextFlags.CallbackCharFilter, ProjectIdentifierFilter))
                {
                    HasCustomisedId = true;
                }
                ImGui.SameLine();
                ImGuiPlus.RequiredMarker();
                ImGui.SameLine();
                ImGuiPlus.HelpMarker("The project identifier (project ID) is essential for internal functioning. It cannot be changed in the future, choose wisely!");
            ImGui.EndGroup();

            ImGui.BeginGroup();
                ImGui.InputTextWithHint("Version name", "'snapshot-0.1.2', '1.0.0.0', '0.15.941'...", ref projectVersion, 16, ImGuiInputTextFlags.CallbackCharFilter, VersionFilter);
                ImGui.SameLine();
                ImGuiPlus.RequiredMarker();
                ImGui.SameLine();
                ImGuiPlus.HelpMarker("Working with semver (semantic versioning).");
            ImGui.EndGroup();
        }

        if (HasCustomisedPath is false)
            this.projectPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{projectName}\\";
        if (HasCustomisedFile is false)
            this.projectFileName = projectName != "" ? $"{projectName.Trim().Replace(" ", "")}.rift" : "";
        if (HasCustomisedId is false)
            this.projectId = this.projectName != "" & this.projectAuthor != "" ? $"com.{projectAuthor}.{projectName}".ToLower().Trim().Replace(" ", "_") : "";

        ImGui.InputTextMultiline("Description", ref projectDescription, 1024, new System.Numerics.Vector2(520, 300));

        ImGui.BeginGroup();
            if (ImGui.Button("Cancel")) isOpen = false;
            ImGui.SameLine();
            if (ImGui.Button("Proceed"))
            {
                if (this.projectName == "") 
                {
                    Console.Write("Missing project name !");
                } 
                else if (this.projectPath == "")
                {
                    Console.Write("Missing project path !");
                }
                else if (this.projectFileName == "")
                {
                    Console.WriteLine("Missing project filename !");
                }
                else if (this.projectAuthor == "")
                {
                    Console.WriteLine("Missing project author !");
                }
                else if (this.projectId == "")
                {
                    Console.WriteLine("Missing project ID !");
                }
                else
                {
                    window.openedProject = new Project(this.projectId)
                    {
                        Name = this.projectName,
                        Author = this.projectAuthor,
                        Description = this.projectDescription,
                        Version = this.projectVersion
                    };

                    window.openedProject.SaveToFile(Path.Combine(projectPath, projectFileName));
                    isOpen = false;
                }
            }
        ImGui.EndGroup();
    }

    public override void RenderAsWindow(float deltaTime)
    {
        ImGui.SetNextWindowSize(new System.Numerics.Vector2(800, 600));
        base.RenderAsWindow(deltaTime);
    }
}
