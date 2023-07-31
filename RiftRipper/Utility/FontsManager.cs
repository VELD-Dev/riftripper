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

    public static ImFontPtr LoadDefaultFont(string fontName, string fontPath, float size = 15f)
    {
        ImFontConfig config = new ImFontConfig()
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

        if(Fonts.ContainsKey(fontName))
        { 
            if(Fonts.TryGetValue(fontName, out Font))
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
        catch(Exception e)
        {
            Console.WriteLine($"Failed to add font {fontName} ({fontPath}).\nError:{e}");
            Fonts.Remove(fontName);
            Font = null;
            return false;
        }
    }
}
