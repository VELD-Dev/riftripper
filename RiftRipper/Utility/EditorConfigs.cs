namespace RiftRipper.Utility;

public class EditorConfigs
{
#if WINDOWS
    public bool AskExtensionAssignation;
#endif
    public bool DebugMode;
    public float RenderDistance;
    public float MoveSpeed;
    public float MaxSpeed;
    public Dictionary<string, string> CustomShaders;

    [JsonIgnore]
    public string SettingsFilePath { get; private set; }

    [JsonConstructor]
    public EditorConfigs()
    {
#if WINDOWS
        AskExtensionAssignation = true;
#endif
        DebugMode = false;
        RenderDistance = 300f;
        MoveSpeed = 10f;
        MaxSpeed = MoveSpeed * (4f/3f);
        CustomShaders = new Dictionary<string, string>();
    }

    public static EditorConfigs LoadSettingsFromFile(string path)
    {
        EditorConfigs configToLoad;
        if (File.Exists(path))
        {
            configToLoad = JsonConvert.DeserializeObject<EditorConfigs>(File.ReadAllText(path));
            configToLoad.SettingsFilePath = path;
        }
        else
        {
            configToLoad = null;
        }
        return configToLoad;
    }

    /// <summary>
    /// Save the settings of the editor to file. Overwrites the entire file, normally that should cause any problem.
    /// </summary>
    /// <returns>A reference to this instance.</returns>
    /// <exception cref="IOException">Occurs if the <see cref="EditorConfigs.SettingsFilePath"/> is null. This should NEVER happen.</exception>
    public EditorConfigs SaveSettingsToFile()
    {
        string output = JsonConvert.SerializeObject(this, Formatting.Indented);

        if (SettingsFilePath == null)
            throw new IOException("The settings file path does not exist! This should NEVER happen!");

        File.WriteAllText(SettingsFilePath, output);
        return this;
    }

    public EditorConfigs ReloadSettings()
    {
        JsonConvert.PopulateObject(File.ReadAllText(SettingsFilePath), this);
        return this;
    }

    /// <summary>
    /// Try loading the editor config from file.
    /// </summary>
    /// <param name="path">Path to the config file.</param>
    /// <param name="config">A reference to the instance of the EditorConfigs.</param>
    /// <returns>True if the file exists, otherwise false.</returns>
    public static bool TryLoadSettingsFromFile(string path, out EditorConfigs config)
    {
        if(File.Exists(path))
        {
            config = JsonConvert.DeserializeObject<EditorConfigs>(File.ReadAllText(path));
            config.SettingsFilePath = path;
            return true;
        }
        else
        {
            config = null;
            return false;
        }
    }

    /// <summary>
    /// Try to load the settings from file, otherwise create the settings file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>A reference to the instance of the EditorConfigs.</returns>
    public static EditorConfigs TryLoadOrCreateSettings(string path)
    {
        if (TryLoadSettingsFromFile(path, out EditorConfigs config))
        {
            config.SettingsFilePath = path;
            return config;
        }

        config = new EditorConfigs() { SettingsFilePath = path };
        return config.SaveSettingsToFile();
    }
}
