namespace RiftRipper.Utility;

public enum Games
{
    RC16,
    Spiderman,
    SpidermanMM,
    RCRiftApart,
    SpidermanRemastered,
    Spiderman2,
    Undefined
}

public class Project
{

    public readonly string Id;
    public required string Name;
    public required string Author;
    public required string SourceLevelPath;
    public Games Game = Games.Undefined;
    public string Description;
    public string Version;
    public string AuthorUrl = string.Empty;
    public string ProjectFilePath { get; private set; }

    [SetsRequiredMembers]
    public Project(string id)
    {
        this.Id = id;
    }

    [SetsRequiredMembers]
    [JsonConstructor]
    public Project(string name, string description, string version, string author, string id, string authorUrl = null) : this(id)
    {
        Name = name;
        Description = description;
        Version = version;
        Author = author;
        AuthorUrl = authorUrl;
    }

    public Project ReloadProject()
    {
        if (ProjectFilePath == null)
        {
            var ex = new NullReferenceException("The project file path is null.");
            ErrorHandler.Alert(ex);
            return null;
        }

        JsonConvert.PopulateObject(File.ReadAllText(ProjectFilePath), this);
        return this;
    }

    public Project SaveToFile(string path)
    {
        string output = JsonConvert.SerializeObject(this, Formatting.Indented);

        File.WriteAllText(path, output);
        ProjectFilePath = path;
        return this;
    }

    public static Project OpenFromFile(string path)
    {
        if(File.Exists(path))
        {
            Project projectToLoad = JsonConvert.DeserializeObject<Project>(File.ReadAllText(path));
            projectToLoad.ProjectFilePath = path;
            return projectToLoad;
        }
        else
        {
            ErrorHandler.Alert("File or path does not exist!");
            return null;
        }
    }

    /// <summary>
    /// Try opening a project from file.
    /// </summary>
    /// <param name="path">Path to the rift file.</param>
    /// <param name="project">Null if the file does not exist, otherwise an instance of the project.</param>
    /// <returns>True if the file exists, false if not.</returns>
    public static bool TryOpenFromFile(string path, out Project project)
    {
        if(!path.EndsWith(".rift"))
        {
            Console.WriteLine("The provided file is not a .rift project file !");
            project = null;
            return false;
        }
        if (File.Exists(path))
        {
            try
            {
                project = JsonConvert.DeserializeObject<Project>(File.ReadAllText(path));
                project.ProjectFilePath = path;
                return true;
            }
            catch(Exception ex)
            {
                ErrorHandler.Alert(ex.Message);
            }
        }
        project = null;
        return false;
    }
}
