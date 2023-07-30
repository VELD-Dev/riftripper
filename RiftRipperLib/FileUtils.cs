using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipperLib;

/// <summary>
/// A manager for the game files.
/// </summary>
public class FilesUtils
{
    public const string TocFileExceptionID = "ERR_TOC_NOT_FOUND";
    public const string DagFileExceptionID = "ERR_DAG_NOT_FOUND";

    public static void ExtractFile(string relativePathToFile)
    {

    }

    /// <summary>
    /// Repacks a file in a specific place in the game files.
    /// </summary>
    /// <param name="pathToFile">Path to the file to repack</param>
    /// <param name="bundleName">Name of the bundle to repack the file into. If it does not exist, a new one will be created and repertoriated into the DAG and TOC files.</param>
    /// <param name="relativePathToFile">Path inside the </param>
    public static void RepackFile(string pathToFile, string bundleName, string relativePathToFile)
    {
        
    }
}
