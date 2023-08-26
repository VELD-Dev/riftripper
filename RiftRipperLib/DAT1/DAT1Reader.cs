using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiftRipperLib.DAT1.Sections;

namespace RiftRipperLib.DAT1;

public class DAT1Reader
{
    public enum SectionHashes : uint
    {
        ArchiveFileSection      =       0x398ABFF0,
        AssetHashSection        =       0x506D7B8A,
        FileLocationSection     =       0x65BCF461,
        StringListSection       =       0xD101A6CC,
        AssetGroupingSection    =       0xEDE8ADA9
    }

    public Header Header;

    public StreamHelper Stream;

    public Dictionary<uint, Section> Sections { get { return Header.sections; } }

    /// <summary>
    /// This is assuming the filestream has already the right offset.
    /// </summary>
    /// <param name="filestream"></param>
    public DAT1Reader(StreamHelper filestream)
    {
        Console.WriteLine("Started reading a DAT1 file.");
        Stream = filestream;
        Header = new Header(filestream);
    }

    public bool TryGetSection<T>(out T? result) where T : DAT1.Section
    {
        result = null;
        switch(typeof(T))
        {
            case T when typeof(T) == typeof(ArchiveFileSection):
                if (!Sections.TryGetValue((uint)SectionHashes.ArchiveFileSection, out var foundSection))
                    return false;
                result = (T)foundSection;
            break;
            default:
                Console.WriteLine("This section is not supported yet.");
                return false;
        }
        return true;
    }
}
