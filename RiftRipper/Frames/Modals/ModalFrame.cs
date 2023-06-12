using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Frames.Modals;

public abstract class Modal
{
    protected Window window;
    protected abstract string frameName { get; set; }
    protected abstract ImGuiWindowFlags window_flags { get; set; }
    public bool isOpen = true;
    private uint frameId;
    private static uint frameIDSource { get { return FRAME_ID_SOURCE++; } }
    private static uint FRAME_ID_SOURCE = 0;

    /// <summary>
    /// Create a new frame
    /// </summary>
    /// <param name="window">Window data</param>
    public Modal(Window window)
    {
        this.window = window;
        frameId = frameIDSource;
        SetWindowTitle(frameName);
    }

    /// <summary>
    /// Sets the name of the frame
    /// </summary>
    /// <param name="title">Name of the frame (# is not allowed)</param>
    protected void SetWindowTitle(string title)
    {
        frameName = $"{title} ###{frameId}";
    }

    /// <summary>
    /// Render the frame
    /// </summary>
    /// <param name="deltaTime">Delta time</param>
    public abstract void Render(float deltaTime);

    /// <summary>
    /// Render the frame as a window
    /// </summary>
    /// <param name="deltaTime">Delta time</param>
    public virtual void RenderAsWindow(float deltaTime)
    {
        if (ImGui.BeginPopupModal(frameName, ref isOpen, window_flags))
        {
            Render(deltaTime);
            ImGui.EndPopup();
        }
    }
}
