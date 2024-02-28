using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility;

unsafe public static class FontsManager
{
    public static readonly ImFontPtr KanitMedium = ImGui.GetIO().Fonts.AddFontFromFileTTF(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "Fonts", "Kanit", "Kanit-Medium.ttf"), 15);
    public static readonly ImFontPtr KanitBold = ImGui.GetIO().Fonts.AddFontFromFileTTF(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "Fonts", "Kanit", "Kanit-Bold.ttf"), 15);
    public static Dictionary<string, ImFontPtr> Fonts { get; private set; } = new()
    {
        { "KanitMedium", KanitMedium },
        { "KanitBold",  KanitBold }
    };

    /// <summary>
    /// Discover fonts in the dir and subdirs for the path.
    /// </summary>
    /// <param name="path">Full path to the file.</param>
    /// <returns>Array of paths pointing to the fonts files.</returns>
    public static string[] DiscoverFonts(string path)
    {
        var filepaths = Directory.GetFiles(path).ToList();
        List<string> fontpaths = new();
        foreach (var filepath in filepaths)
        {
            if (!File.Exists(filepath))
                continue;

            if (!filepath.EndsWith(".ttf") || !filepath.EndsWith(".otf"))
                continue;

            fontpaths.Add(filepath);
        }
        var subdirs = Directory.GetDirectories(path).ToList();
        foreach (var dir in subdirs)
        {
            if (!Directory.Exists(dir))
                continue;
            filepaths = Directory.GetFiles(dir).ToList();
            foreach (var filepath in filepaths)
            {
                if (!File.Exists(filepath))
                    continue;

                if (!filepath.EndsWith(".ttf") || !filepath.EndsWith(".otf"))
                    continue;

                if (fontpaths.Contains(filepath) || fontpaths.Any(str => str.Split(Path.PathSeparator).Last() == filepath.Split(Path.PathSeparator).Last()))
                    continue;

                fontpaths.Add(filepath);
            }
        }
        return fontpaths.ToArray();
    }

    public static ImFontPtr LoadDefaultFont(string fontName, string fontPath, float size = 15f)
    {
        ImFontConfig config = new()
        {
            FontDataOwnedByAtlas = 0,
            RasterizerMultiply = 1
        };
        var font = ImGui.GetIO().Fonts.AddFontFromFileTTF(fontPath, size, &config);
        Fonts.Add(fontName, font);
        return font;
    }

    public static bool TryAddFont(string fontName, string fontPath, out ImFontPtr Font, float size = 15f)
    {
        if (!File.Exists(fontPath))
        { Font = null; return false; }

        if (!fontPath.EndsWith(fontPath))
        { Font = null; return false; }

        if (string.IsNullOrEmpty(fontName))
        { Font = null; return false; }

        if (Fonts.ContainsKey(fontName))
        {
            if (Fonts.TryGetValue(fontName, out Font))
                return true;
            return false;
        }

        try
        {
            var font = ImGui.GetIO().Fonts.AddFontFromFileTTF(fontPath, size);
            Fonts.Add(fontName, font);
            Font = font;
            return true;
        }
        catch (Exception e)
        {
            ErrorHandler.Alert($"Failed to add font {fontName} ({fontPath}).\n{e}");
            Fonts.Remove(fontName);
            Font = null;
            return false;
        }
    }
}
