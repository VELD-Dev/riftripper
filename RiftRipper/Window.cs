using System.Reflection;
using RiftRipper.Utility.Luna;
using Vector2 = System.Numerics.Vector2;

namespace RiftRipper;

public class Window : GameWindow
{
    public static Window Singleton = null;
    public string oglVersionString = "Unkown OpenGL version";
    private ImGuiController controller;
    private List<Frame> openFrames;
    public Project openedProject;
    public Games openedGame = Games.Undefined;
    public string openedGamePath = string.Empty;
    public EditorConfigs Settings = EditorConfigs.TryLoadOrCreateSettings(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Settings.json"));
    internal bool showFramerate = false;
    internal float Framerate;
    internal Vector4 screenSafeSpace;
    internal DAT1Manager DAT1Manager;


    public string[] args { get; private set; }

    public Window(string[] args) : base(GameWindowSettings.Default,
        new() { Size = new(1600, 900), APIVersion = new(4, 6), Flags = ContextFlags.ForwardCompatible, Profile = ContextProfile.Core })
    {
        this.args = args;
        this.VSync = VSyncMode.On;
        openFrames = new List<Frame>();
        Window.Singleton = this;
    }

    public void AddFrame(Frame frame)
    {
        openFrames.Add(frame);
    }

    public bool IsAnyFrameOpened<T>() where T : Frame
    {
        return openFrames.Any(f => f.GetType() == typeof(T));
    }

    public void TryCloseFirstFrame<T>() where T : Frame
    {
        if (IsAnyFrameOpened<T>())
        {
            var frameToClose = openFrames.First(f => f.GetType() == typeof(T));
            frameToClose.isOpen = false;
        }
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

        screenSafeSpace = new(0, 0, ImGui.GetMainViewport().Size.X, ImGui.GetMainViewport().Size.Y);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.Texture2D);

        MaterialManager.LoadMaterial("standard.vert;standardunlit.frag", "Shaders/standard.vert.glsl", "Shaders/standardunlit.frag.glsl");
        //MaterialManager.LoadMaterial("standard.vert;standardwhite.frag", "Shaders/standard.vert.glsl", "Shaders/standardwhite.frag.glsl");

        if (args.Length > 0)
        {
            foreach (string arg in args)
            {
                string argname = arg.Split("=")[0];
                string argvalue = arg.Split("=").Length == 2 ? arg.Split("=")[1] : null;

                if (argvalue is not null)
                {
                    switch (argname.ToUpper())
                    {
                        case "-PATH":
                            openedGamePath = argvalue;
                            if (openedGamePath.EndsWith(".exe"))
                            {
                                ErrorHandler.Alert("The provided Path is not valid! It must be the game folder.");
                                break;
                            }
                            DAT1Manager = new(openedGamePath);
                            break;
                        case "-PROJECT":
                            if (Project.TryOpenFromFile(argvalue, out var proj))
                            {
                                openedProject = proj;
                            }
                            else
                            {
                                ErrorHandler.Alert("The provided Project is not valid!");
                            }
                            break;
                        default:
                            Console.WriteLine(
                                $"Unknown argument '{argname}' with value '{argvalue}'\n" +
                                 "Case does not matter for arguments name. Allowed arguments:\n" +
                                 "\t-Path='P:\\ath\\To\\The\\File.ext' -- Loads instantly the game.\n" +
                                 "\t-Project='P:\\ath\\To\\Project.rift' -- Loads instantly a project.\n" +
                                 "\t-Transparency=<true/false> -- Wether the level should load with transparency instantly. Can be changed later in editor settings." +
                                 "\t-CustomShader='P:\\ath\\To\\The\\File.glsl' -- Load a custom shader added over the existing ones. Can be changed later in the editor settings."
                                );
                            break;
                    }
                }
                else
                {
                    Program.ProvidedPath = argname;
                }
            }
        }

        // Setting ImGui default settings
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 5f);
        ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, 5f);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 2.5f);
        ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, 2.5f);

#if WIN
        if (Settings.AskExtensionAssignation && !ExtensionManager.IsAssociated(".rift", Program.AppName))
        {
            AddFrame(new AssignExtensionModal(this));
        }
#endif
        ImFontPtr ftptr = ImGui.GetIO().Fonts.AddFontFromFileTTF(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "Fonts", "Kanit", "Kanit-Regular.ttf"), 15);
        ImGui.PushFont(ftptr);
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

        if (showFramerate)
        {
            Overlays.ShowOverlay(this, showFramerate);
        }

        Title = openedProject is not null ? $"RiftRipper {Program.version} ({oglVersionString}) - {openedProject.Name} {openedProject.Version} by {openedProject.Author}" : $"RiftRipper {Program.version} ({oglVersionString})";

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        GL.ClearColor(new Color4(48, 48, 48, 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        controller?.Render();

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        if (showFramerate)
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

    private void RenderUI(float deltaTime)
    {
        RenderMenuBar();
        RenderDockSpace();

        foreach (Frame frame in openFrames.ToList())
            frame.RenderAsWindow(deltaTime);
    }

    private void RenderDockSpace()
    {
        ImGuiWindowFlags windowFlags = ImGuiWindowFlags.NoDocking
            | ImGuiWindowFlags.NoTitleBar
            | ImGuiWindowFlags.NoCollapse
            | ImGuiWindowFlags.NoResize
            | ImGuiWindowFlags.NoMove
            | ImGuiWindowFlags.NoBringToFrontOnFocus
            | ImGuiWindowFlags.NoNavFocus;
        ImGui.SetNextWindowViewport(ImGui.GetWindowViewport().ID);
        ImGui.SetNextWindowPos(ImGui.GetMainViewport().WorkPos);
        ImGui.SetNextWindowSize(ImGui.GetMainViewport().WorkSize);

        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, 0);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
        ImGui.Begin("dockspace", windowFlags);
        ImGui.PopStyleVar();

        uint dockspaceId = ImGui.GetID("dockspace");
        ImGui.DockSpace(dockspaceId, new Vector2(0, 0), ImGuiDockNodeFlags.None);
    }

    private void RenderMenuBar()
    {
        if (!ImGui.BeginMainMenuBar())
            return;

        if (ImGui.BeginMenu("File"))
        {
            FileMenuDraw.OpenGameMenuItem(this);
            ImGui.Separator();
            FileMenuDraw.CreateRiftProjectMenuItem(this);
            FileMenuDraw.OpenRiftProjectMenuItem(this);
            FileMenuDraw.CloseActiveProjectMenuItem(this);
            ImGui.Separator();
            FileMenuDraw.CloseRiftripperMenuItem(this);
            ImGui.EndMenu();
        }
        if (ImGui.BeginMenu("Edit"))
        {
            EditMenuDraw.EditorSettingsMenuItem(this);
            EditMenuDraw.ProjectSettingsMenuItem(this);
            ImGui.Separator();
            if (ImGui.BeginMenu("Tools"))
            {
                EditMenuDraw.ToolsMenuDraw.CreateLocalPortalMenuItem(this);
                EditMenuDraw.ToolsMenuDraw.CreateDisplacementPortalMenuItem(this);
                EditMenuDraw.ToolsMenuDraw.CreateLevelPortalMenuItem(this);
                EditMenuDraw.ToolsMenuDraw.CreatePocketRiftPortalMenuItem(this);
                ImGui.EndMenu();
            }
            ImGui.EndMenu();
        }
        if (ImGui.BeginMenu("View"))
        {
            ImGui.SeparatorText("Overlay");
            ViewMenuDraw.ShowFramerateMenuItem(this);

            ImGui.SeparatorText("Windows");
            ViewMenuDraw.ShowArchiveExplorer(this);
            ImGui.EndMenu();
        }
        if (ImGui.BeginMenu("About"))
        {
            AboutMenuDraw.OfficialGithubRepoMenuItem(this);
            ImGui.EndMenu();
        }
        DebugMenuDraw.Menu(this);
        screenSafeSpace.Y = ImGui.GetWindowSize().Y - 1;
        screenSafeSpace.W = ImGui.GetMainViewport().Size.Y - ImGui.GetWindowSize().Y - 1;
        ImGui.EndMainMenuBar();
    }
}
