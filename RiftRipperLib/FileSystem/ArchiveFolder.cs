using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib.FileSystem;

public class ArchiveFolder : ArchiveNode
{
    public override string Name { get; set; }
    public override DateTime CreateDate { get; set; }
    public override string Type { get; set; } = "Folder";

    public FieldBasedList<ArchiveNode> Children { get; private set; }

    public bool TryAddChild(ArchiveNode node)
    {
        try
        {
            Children.Add(node);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
