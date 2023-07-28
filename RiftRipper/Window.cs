using FileDialog = RiftRipper.Utility.FileDialog;

namespace RiftRipper;

public class Window : GameWindow
{
    public string oglVersionString = "Unkown OpenGL version";
    private ImGuiController controller;
    private List<Frame> openFrames;
    public Project openedProject;
    public EditorConfigs Settings = EditorConfigs.TryLoadOrCreateSettings(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings.json"));
    internal bool showFramerate = false;
    internal float Framerate;


    public string[] args { get; private set; }

    public Window(string[] args) : base(GameWindowSettings.Default,
        new() { Size = new(1600, 900), APIVersion = new(4, 6), Flags = ContextFlags.ForwardCompatible, Profile = ContextProfile.Core })
    {
        this.args = args;
        this.VSync = VSyncMode.On;
        openFrames = new List<Frame>();
    }

    public void AddFrame(Frame frame)
    {
        openFrames.Add(frame);
    }

    public static bool FrameMustClose(Frame frame)
    {
        return !frame.isOpen;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        // TODO: Add a "Do not forget to save!" window after if there's a project.
        Environment.Exit(0);
        base.OnClosing(e);
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        oglVersionString = $"OpenGL {GL.GetString(StringName.Version)}";
        Title = $"RiftRipper {Program.version} ({oglVersionString})";

        controller = new ImGuiController(ClientSize.X, ClientSize.Y);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Texture2D);

        MaterialManager.LoadMaterial("standard.vert;standardunlit.frag", "Shaders/standard.vert.glsl", "Shaders/standardunlit.frag.glsl");
        //MaterialManager.LoadMaterial("standard.vert;standardwhite.frag", "Shaders/standard.vert.glsl", "Shaders/standardwhite.frag.glsl");

        if(args.Length > 0)
        {
            foreach(string arg in args)
            {
                string argname = arg.Split("=")[0];
                string argvalue = arg.Split("=").Length == 2 ? arg.Split("=")[1] : null;

                if(argvalue is not null)
                {
                    switch (argname)
                    {
                        case "PATH":
                            Program.ProvidedPath = argvalue;
                            break;
                        default:
                            Console.Write(
                                $"Unknown argument '{argname}' with value '{argvalue}'\n"+
                                 "Case does not matter for arguments name. Allowed arguments:\n"+
                                 "\t\t\t\t- Path='P:\\ath\\To\\The\\File.ext' -- Loads instantly the level or the whole game.\n"+
                                 "\t\t\t\t- Transparency=<true/false> -- Wether the level should load with transparency instantly. Can be changed later in editor settings."+
                                 "\t\t\t\t- CustomShader='P:\\ath\\To\\The\\File.glsl' -- Load a custom shader added over the existing ones. Can be changed later in the editor settings."
                                );
                            break;
                    }
                } else
                {
                    Program.ProvidedPath = argname;
                }
            }
        }

        FontsManager.LoadDefaultFont("KanitRegular", Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Assets", "Fonts", "Kanit", "Kanit-Regular.ttf"));

        // Setting ImGui default settings
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 5f);
        ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, 5f);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 2.5f);
        ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, 2.5f);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        // Update OGL viewport
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

        // Resize ImGUI interface with window
        controller?.WindowResized(ClientSize.X, ClientSize.Y);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        openFrames.RemoveAll(FrameMustClose);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

        controller?.Update(this, (float)args.Time);

        RenderUI((float)args.Time);

        if(showFramerate)
        {
            Overlays.ShowOverlay(this, showFramerate);
        }

        Title = openedProject is not null ? $"RiftRipper {Program.version} ({oglVersionString}) - Project {openedProject.Name} {openedProject.Version} by {openedProject.Author}" : $"RiftRipper {Program.version} ({oglVersionString})";

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        GL.ClearColor(new Color4(48, 48, 48, 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        controller?.Render();

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        if(showFramerate)
            Framerate = (float)Math.Round(1 / (float)args.Time, 1);

        base.OnUpdateFrame(args);
    }

    protected override void OnTextInput(TextInputEventArgs e)
    {
        base.OnTextInput(e);

        controller?.PressChar((char)e.Unicode);
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        controller?.MouseScroll(e.Offset);
    }

    private void RenderMenuBar()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Open level", "CTRL+L"))
                {
                    var res = FileDialog.OpenFile();
                    if (res.Length > 0)
                    {
                        Console.WriteLine($"File found: {res[0]}");
                    }
                }

                if (ImGui.MenuItem("Open a game", "CTRL+G"))
                {
                    var res = FileDialog.OpenFolder();
                    if (res.Length > 0)
                    {
                        Console.WriteLine($"Folder found: {res[0]}, must check if it's a game");
                    }
                }

                ImGui.Separator();

                if (ImGui.MenuItem("Create a Rift project", "CTRL+P"))
                {
                    AddFrame(new CreateProjectFrame(this));
                    Console.WriteLine("Should open project creation menu");
                }

                if (ImGui.MenuItem("Open a Rift project (.rift)", "CTRL+ALT+P"))
                {
                    var res = FileDialog.OpenFile("Open project", ".rift");
                    if (res.Length > 0)
                    {
                        try
                        {
                            this.openedProject = Project.OpenFromFile(res);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }

                if(openedProject != null)
                {
                    if(ImGui.MenuItem("Close the active project", "SHIFT+ALT+P"))
                        openedProject = null;
                }

                ImGui.Separator();

                if(ImGui.MenuItem("Quit", "ALT+F4"))
                {
                    Environment.Exit(0);
                }

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Edit"))
            {
                if (ImGui.MenuItem("Editor settings", "CTRL+SHIFT+E"))
                {
                    AddFrame(new EditorSettingsFrame(this));
                    Console.WriteLine("Should open editor settings");
                }

                if (ImGui.MenuItem("Project settings", "CTRL+SHIFT+P", false, openedProject != null))
                {
                    AddFrame(new ProjectSettingsFrame(this, openedProject));
                    Console.WriteLine("Should open project settings");
                }

                ImGui.Separator();

                if (ImGui.BeginMenu("Tools"))
                {
                    if (ImGui.MenuItem("Create a local portal (R&C:RA only)", "P", false, false))
                    {
                        Console.WriteLine("Opening portal creation modal !");
                    }

                    if(ImGui.MenuItem("Create a displacement portal (R&C:RA only)", "SHIFT+P", false, false))
                    {
                        Console.WriteLine("Opening displacement portal creation modal !");
                    }

                    if (ImGui.MenuItem("Create a level portal (R&C:RA only)", "L", false, false))
                    {
                        Console.WriteLine("Opening levels portal creation modal !");
                    }

                    if (ImGui.MenuItem("Create a pocket rift portal (R&C:RA only)", "SHIFT+L", false, false))
                    {
                        Console.WriteLine("Opening pocket rift portal creation modal !");
                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("View"))
            {
                if (ImGui.MenuItem("Show framerate", "", showFramerate, true))
                    showFramerate = !showFramerate;

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("About"))
            {
                if (ImGui.MenuItem("Official GitHub repository"))
                {
                    Process.Start(new ProcessStartInfo("https://github.com/VELD-Dev/riftripper") { UseShellExecute = true });
                }
                ImGui.EndMenu();
            }

            if (Settings.DebugMode)
            {
                if (ImGui.BeginMenu("Debug"))
                {
                    if (ImGui.MenuItem("Demo frame"))
                    {
                        AddFrame(new DemoWindowFrame(this));
                    }
                    ImGui.EndMenu();
                }
            }

            ImGui.EndMainMenuBar();
        }
    }

    private void RenderUI(float deltaTime)
    {
        RenderMenuBar();

        foreach (Frame frame in openFrames)
            frame.RenderAsWindow(deltaTime);
    }
}
