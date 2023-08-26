using RiftRipperLib.FileSystem;
using RiftRipperLib.DAT1.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vulkan.Win32;
using Vector2 = System.Numerics.Vector2;

namespace RiftRipper.Frames.Dockable;

internal class ArchiveExplorerFrame : DockableFrame 
{
    protected override string frameName { get; set; } = "Archive explorer";
    protected override ImGuiWindowFlags window_flags { get; set; } = ImGuiWindowFlags.None;
    protected override Vector2 default_position { get; set; } = ImGui.GetMainViewport().GetCenter();
    protected override ImGuiCond docking_condition { get; set; } = ImGuiCond.FirstUseEver;

    internal string currentPath = @"\";
    private string _tempPath = @"\";
    internal ArchiveNode rootNode;
    internal ArchiveNode currentNode;

    public ArchiveExplorerFrame(Window wnd) : base(wnd)
    {
        if (!wnd.DAT1Manager.TOC.TryGetSection(out ArchiveFileSection archiveSection))
            return;

        archiveSection = archiveSection.ReadValues(wnd.DAT1Manager.TOC.Stream);
        rootNode = archiveSection.RootNode ?? new ArchiveFolder() { Name = "", Path = @"\", Type = "Folder" };
        currentNode = rootNode;
    }

    public override void Render(float deltaTime)
    {

        ImGui.BeginGroup();
        ImGui.Text("Path:");
        ImGui.SameLine();
        bool pathChanged = ImGui.InputText("", ref _tempPath, 0xFF, ImGuiInputTextFlags.EnterReturnsTrue);
        if(pathChanged)
        {
            bool _folderFound = ChangeCurrentNodeByPath(_tempPath);
        }
        ImGui.EndGroup();

        ImGui.BeginGroup();
        RenderNode(currentNode);
        ImGui.EndGroup();

        ImGui.SameLine();
        ImGui.BeginChild("Preview", new Vector2(200, 0), true);

        ImGui.EndChild();
    }

    public void RenderNode(ArchiveNode node)
    {
        if (node == null)
            return;

        if (node.Type == "Folder")
            return;

        ArchiveFolder _node = node as ArchiveFolder;

        string label = $"{_node.Name}";
        bool nodeIsOpen = ImGui.TreeNodeEx(label, ImGuiTreeNodeFlags.OpenOnDoubleClick);

        if (nodeIsOpen)
        {
            currentPath = _node.Path;

            foreach (var childNode in _node.Children)
            {
                RenderNode(childNode);
            }

            ImGui.TreePop();
        }
    }

    public override void RenderAsWindow(float deltaTime)
    {
        base.RenderAsWindow(deltaTime);
    }

    private bool ChangeCurrentNodeByPath(string path)
    {
        string[] pathSegments = path.Split('/');

        ArchiveNode _currentNode = rootNode;

        foreach (string segment in pathSegments)
        {
            if (currentNode is ArchiveFolder currentFolder)
            {
                ArchiveNode foundNode = currentFolder.Children.FirstOrDefault(child => child.Name == segment);
                if (foundNode != null)
                {
                    currentNode = foundNode;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false; // Le chemin a dépassé un fichier (non géré ici)
            }
        }

        currentNode = _currentNode;
        return true;
    }
}
