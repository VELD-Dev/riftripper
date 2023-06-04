namespace RiftRipper.Frames;

/// <summary>
/// Frame (window) inside the viewport
/// </summary>
public abstract class Frame
{
    protected Window window;
    protected abstract string frameName { get; set; }
    public bool isOpen = true;
    private uint frameId;
    private static uint frameIdSource { get { return FRAME_ID_SOURCE++; } }
    private static uint FRAME_ID_SOURCE = 0;

    /// <summary>
    /// Create a new frame
    /// </summary>
    /// <param name="window">Window data</param>
    public Frame(Window window)
    {
        this.window = window;
        frameId = frameIdSource;
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
        if(ImGui.Begin(frameName, ref isOpen))
        {
            Render(deltaTime);
            ImGui.End();
        }
    }
}
