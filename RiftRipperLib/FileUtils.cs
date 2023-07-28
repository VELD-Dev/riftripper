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
    public const string TocFileExceptionID = "ERR_TOC_FILE_ABSENT";
    public const string DagFileExceptionID = "ERR_DAG_FILE_ABSENT";
    public static void ExtractFiles(string pathToDdir)
    {
        IList<string> errors = new List<string>();
        var tocFilePath = Path.Combine(Path.GetFullPath(pathToDdir), "../", "toc");
        var dagFilePath = Path.Combine(Path.GetFullPath(pathToDdir), "../", "dag");

        if (!File.Exists(tocFilePath))
            errors.Add($"{TocFileExceptionID}: No TOC file found in the game root directory.");
        if
    }

    public static void ExtractFile(string pathToFile,)

    public static void RepackFiles(string pathToDdir)
    {
        
    }
}
