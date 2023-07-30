using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;

public class DagHandler
{
    public enum Section
    {
        ArchiveFile,
        AssetHash,
        FileLocation,
        StringList,
        AssetGrouping
    }
    public static readonly Dictionary<Section, long> Sections = new()
    {
        { Section.ArchiveFile,      0x398ABFF0 },
        { Section.AssetHash,        0x506D7B8A },
        { Section.FileLocation,     0x65BCF461 },
        { Section.StringList,       0xD101A6CC },
        { Section.AssetGrouping,    0xEDE8ADA9 },
    };
}
