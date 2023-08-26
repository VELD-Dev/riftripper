using RiftRipperLib.FileSystem;

namespace RiftRipperLib.DAT1.Sections;

public class ArchiveFileSection : Section
{
    public List<string> paths = new();
    public ArchiveFolder RootNode { get; private set; } = new ArchiveFolder();

    public ArchiveFileSection(StreamHelper filestream) : base(filestream) { }

    public ArchiveFileSection ReadValues(StreamHelper filestream)
    {
        List<string> stringPaths = new();

        ulong lastOffset = offset;
        bool hasReachedEnd = false;
        while (!hasReachedEnd)
        {
            filestream.Seek(lastOffset);
            string path = filestream.ReadString(out ulong stringEndOffset, size: 0x44);
            stringPaths.Add(path);
            lastOffset = stringEndOffset;
            if (lastOffset >= End)
                hasReachedEnd = true;
        }
        paths = stringPaths;
        ConstructNodes();
        return this;
    }

    private void ConstructNodes()
    {
        foreach (string path in paths)
        {
            string[] pathSegments = path.Split('/');
            ArchiveFolder? currentNode = RootNode;

            foreach (string segment in pathSegments)
            {
                ArchiveNode? foundNode = currentNode?.Children.FirstOrDefault(child => child.Name == segment);

                if (foundNode == null)
                {
                    ArchiveNode? newNode;

                    if (segment == "")
                    {
                        newNode = RootNode;
                    }
                    else
                    {
                        newNode = new ArchiveFolder
                        {
                            Name = segment,
                            Parent = currentNode,
                            CreateDate = DateTime.Now,
                            Type = "Folder"
                        };
                        currentNode?.Children.Add(newNode);
                    }

                    currentNode = newNode as ArchiveFolder;
                }
                else
                {
                    currentNode = foundNode as ArchiveFolder;
                }
            }
        }
    }
}
