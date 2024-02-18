using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulkan.Win32;

namespace RiftRipper.Frames;

public class ProjectSettingsFrame : Frame
{
    protected override string frameName { get; set; } = "Project settings";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.NoResize;
    public required Project loadedProject { get; set; }

    [SetsRequiredMembers]
    public ProjectSettingsFrame(Window wnd) : base(wnd)
    {
        loadedProject = window.openedProject;
    }

    [SetsRequiredMembers]
    public ProjectSettingsFrame(Window wnd, Project project) : base(wnd)
    {
        loadedProject = project;
    }

    public override void Render(float deltaTime)
    {
        ImGui.BeginGroup();
        ImGui.InputTextWithHint("Project name", "'My first project!', etc...", ref loadedProject.Name, 64);
        ImGui.InputTextWithHint("Project author", "'VELD-Dev, and his clone', etc...", ref loadedProject.Author, 64);
        ImGui.InputTextWithHint("Project author URL", "'https://github.com/VELD-Dev/'", ref loadedProject.AuthorUrl, 256);
        ImGui.InputTextWithHint("Project version", "'0.0.1-snapshot', '1.0.1-release', etc...", ref loadedProject.Version, 24);
        ImGui.InputTextMultiline("Description", ref loadedProject.Description, 1024, new(400, 300));
        ImGui.EndGroup();

        ImGui.Separator();

        ImGui.BeginGroup();
        if(ImGui.Button("Save"))
        {
            loadedProject.SaveToFile(loadedProject.ProjectFilePath);
        }
        ImGui.SameLine();
        if(ImGui.Button("Cancel"))
        {
            loadedProject.ReloadProject();
        }
        ImGui.SameLine();
        if (ImGui.Button("Close"))
        {
            isOpen = !isOpen;
        }
        ImGui.EndGroup();
    }

    public override void RenderAsWindow(float deltaTime)
    {
        if(window.openedProject == null)
        {
            isOpen = false;
            loadedProject = null;
        }

        ImGui.SetNextWindowSize(new System.Numerics.Vector2(800, 600));
        base.RenderAsWindow(deltaTime);
    }

}
