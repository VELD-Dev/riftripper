using RiftRipperLib.DAT1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility.Luna;

internal class DAT1Manager
{
    public DAT1Reader TOC { get; private set; }
    public DAT1Reader DAG { get; private set; }

    public DAT1Manager(string gamePath)
    {
        Console.WriteLine("Started reading TOC and DAG files.");
        if (string.IsNullOrEmpty(gamePath))
            throw new ArgumentNullException($"{nameof(gamePath)} '{gamePath}' argument is empty or null!");

        var dirs = Directory.GetDirectories(gamePath);
        var files = Directory.GetFiles(gamePath);

        Console.WriteLine("Checking TOC and DAG existence.");
        if (!files.Contains(Path.Combine(gamePath, "toc")) || !files.Contains(Path.Combine(gamePath, "dag")))
            throw new FileNotFoundException($"File 'toc' or 'dag' does not exist in the specified directory: {gamePath}");

        var toc = files.First(file => file == Path.Combine(gamePath, "toc"));
        var dag = files.First(file => file == Path.Combine(gamePath, "dag"));

        var tocStream = new StreamHelper(new FileStream(toc, FileMode.Open, FileAccess.Read));
        var dagStream = new StreamHelper(new FileStream(dag, FileMode.Open, FileAccess.Read));

        Console.WriteLine("TOC and DAG files exist, started reading those.");

        Console.WriteLine("Started reading TOC file.");
        tocStream.Seek(0x08);
        TOC = new DAT1Reader(tocStream);

        Console.WriteLine("Started reading DAG file.");
        dagStream.Seek(0x0C);
        DAG = new DAT1Reader(dagStream);
    }
}
