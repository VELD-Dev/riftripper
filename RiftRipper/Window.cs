namespace RiftRipper;

public class Window : GameWindow
{
    public string oglVersionString = "Unkown OpenGL version";

    private ImGuiController controller;
    public string[] args { get; private set; }

    public Window(string[] args) : base(GameWindowSettings.Default,
        new() { Size = new(1600, 900), APIVersion = new(4, 6), Flags = ContextFlags.ForwardCompatible, Profile = ContextProfile.Core, Vsync = VSyncMode.On})
    {
        this.args = args;
        
        
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

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

        controller?.Update(this, (float)args.Time);

        RenderUI((float)args.Time);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        GL.ClearColor(new Color4(48, 48, 48, 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

        controller?.Render();

        SwapBuffers();
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
                    Console.WriteLine("Should open project creation menu");
                }

                if (ImGui.MenuItem("Open a Rift project (.rift)", "CTRL+ALT+P"))
                {
                    var res = FileDialog.OpenFile("Open project", ".rift");
                    if (res.Length > 0)
                    {
                        Console.WriteLine($"File found: {res[0]}");
                    }
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
                    Console.WriteLine("Should open editor settings menu");
                }

                if (ImGui.MenuItem("Project settings", "CTRL+SHIFT+P", false, false))
                {
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
                ImGui.SeparatorText("Coming soon...");
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
            ImGui.EndMainMenuBar();
        }
    }

    private void RenderUI(float deltaTime)
    {
        RenderMenuBar();
    }
}
