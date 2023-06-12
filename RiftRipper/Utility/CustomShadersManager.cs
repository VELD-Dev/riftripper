using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility;

public static class CustomShadersManager
{
    public static void CheckShaders(string customShaderDirPath)
    {
        if(Directory.Exists(customShaderDirPath))
        {
            foreach(var dir in Directory.EnumerateDirectories(customShaderDirPath))
            {
                foreach (var filepath in Directory.EnumerateFiles(customShaderDirPath))
                {
                    var filename = Path.GetFileName(filepath);
                    if (filename != "shaderinfo.json")
                        return;

                    
                }
            }

        }
    }


}
