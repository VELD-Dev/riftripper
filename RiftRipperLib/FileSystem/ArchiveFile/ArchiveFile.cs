using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib.FileSystem.ArchiveFile;

public class ArchiveFile : ArchiveNode
{
    public override string Name { get; set; }
    public override DateTime CreateDate { get; set; }
    public override string Type { get; set; } = "IGFile";
}
