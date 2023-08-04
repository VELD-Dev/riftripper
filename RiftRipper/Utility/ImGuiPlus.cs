using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility;

public static class ImGuiPlus
{
    /// <summary>
    /// Adds a question mark to show additional information.
    /// </summary>
    /// <param name="desc">Content of the tooltip</param>
    public static void HelpMarker(string desc)
    {
        ImGui.TextDisabled("(?)");
        if(ImGui.IsItemHovered(ImGuiHoveredFlags.DelayShort) && ImGui.BeginTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(desc);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    /// <summary>
    /// Add a required marker.
    /// </summary>
    public static void RequiredMarker()
    {
        ImGui.TextColored(new(228f/255f, 48f/255f, 48f/255f, 1.0f), "*");
    }

    public static void RequiredMarker(string desc, ImGuiHoveredFlags hoveredFlags = ImGuiHoveredFlags.DelayShort)
    {
        ImGui.TextColored(new(228f/255f, 48f/255f, 48f/255f, 1.0f), "*");
        if(ImGui.IsItemHovered(hoveredFlags) && ImGui.BeginTooltip())
        {
            ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
            ImGui.TextUnformatted(desc);
            ImGui.PopTextWrapPos();
            ImGui.EndTooltip();
        }
    }

    public struct DirectoryNode
    {
        public string FullPath;
        public string FileName;
        public List<DirectoryNode> Children;
        public bool isDirectory;
    }

    public static DirectoryNode CreateDirNodeTreeFromPath(string rootPath)
    {
        DirectoryNode rootNode;
        rootNode.FullPath = rootPath;
        rootNode.FileName = Path.GetFileName(rootPath);
        rootNode.isDirectory = rootPath.EndsWith("/");
        if(rootNode.isDirectory)
            RecursivelyAddDirNodes(rootNode, )
    }

    public static IEnumerable<DirectoryNode> EnumerateDirectChildren(string currentPath, List<string> allPaths)
    {
        foreach(string path in allPaths)
        {
            if (!path.StartsWith(currentPath))
                continue;

            if (currentPath.Split('\\').Length <= path.Split("/").Length)
                continue;

            if (currentPath.Split('\\').Length >= path.Split("/").Length + 1)
                continue;
            var node = new DirectoryNode();
            node.FullPath = path;
            node.FileName = path;
            node.isDirectory = path.EndsWith("/");
        }
    }

    public static void RecursivelyAddDirNodes()
}
