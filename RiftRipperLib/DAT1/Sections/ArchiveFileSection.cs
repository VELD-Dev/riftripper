namespace RiftRipperLib.DAT1.Sections;

public class ArchiveFileSection : Section
{
    public List<string> paths = new();
    public ArchiveFileSection(StreamHelper filestream) : base(filestream) {}

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
        return this;
    }
}
