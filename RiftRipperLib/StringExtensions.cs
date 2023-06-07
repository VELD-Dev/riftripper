using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;

public static class StringExtensions
{
    public static int? strchr(this string originalString, char searchingChar)
    {
        int? found = originalString.IndexOf(searchingChar);
        return found > -1 ? found : null;
    }
}
