using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility.Luna;

internal class FilesManager
{
    internal DAT1Manager Dat1Manager { get; private set; }

    public FilesManager(DAT1Manager dat1Manager)
    {
        Dat1Manager = dat1Manager;
    }
}
