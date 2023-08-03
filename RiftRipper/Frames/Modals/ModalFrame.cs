using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Frames.Modals;

public abstract class Modal : Frame
{
    protected Modal(Window window) : base(window)
    {
    }
}
