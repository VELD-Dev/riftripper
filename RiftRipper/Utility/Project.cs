using System.Diagnostics.CodeAnalysis;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.Win32.SafeHandles;

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
    public required string Name { get; set; }
    public required string Author { get; set; }
    public required string SourceLevelPath { get; set; }
    public Games Game { get; set; } = Games.Undefined;
    public string Description { get; set; }
    public string Version { get; set; }
    public string AuthorUrl { get; set; } = string.Empty;

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
        Id = id;
        AuthorUrl = authorUrl;
    }

    public Project SaveToFile(string path)
    {
        string output = JsonConvert.SerializeObject(this, Formatting.Indented); 

        File.WriteAllText(path, output);
        return this;
    }

    public static Project OpenFromFile(string path)
    {
        Project projectToLoad = JsonConvert.DeserializeObject<Project>(File.ReadAllText(path));
        return (Project)projectToLoad;
    }
}
