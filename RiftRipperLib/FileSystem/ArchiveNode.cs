using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib.FileSystem;

public abstract class ArchiveNode
{
    public abstract string Name { get; set; }
    public abstract DateTime CreateDate { get; set; }
    public abstract string Type { get; set; }
    public virtual DateTime EditDate { get; set; }
    public virtual string Path { get; set; }
    public ArchiveNode? Parent { get; set; }
    public long Size { get; private set; }
}
