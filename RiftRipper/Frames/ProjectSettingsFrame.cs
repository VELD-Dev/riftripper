﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Frames;

public class ProjectSettingsFrame : Frame
{
    protected override string frameName { get; set; } = "Project settings";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize;
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
        ImGui.Text(loadedProject.Name);
    }

    public override void RenderAsWindow(float deltaTime)
    {
        ImGui.SetWindowSize(new System.Numerics.Vector2(800, 600));
        base.RenderAsWindow(deltaTime);
    }

}
