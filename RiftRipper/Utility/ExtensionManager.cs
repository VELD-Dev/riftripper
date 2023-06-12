#if WINDOWS
using Microsoft.Win32;

namespace RiftRipper.Utility;

/// <summary>
/// WARNING! THIS IS MODIFYING THE WINDOWS REGISTRY ! BE CAREFUL !
/// </summary>
public static class ExtensionManager
{
    public static bool IsAssociated(string ext, string key)
    {
        return Registry.ClassesRoot.OpenSubKey(ext) != null && Registry.ClassesRoot.OpenSubKey(key) != null;
    }

    /// <summary>
    /// Try to dissociate the app and the extension
    /// </summary>
    /// <param name="Extension"></param>
    /// <param name="KeyName"></param>
    /// <returns>True if the dissociation is successful, otherwise false.</returns>
    public static bool TryDissociateExtension(string Extension, string KeyName)
    {
        Registry.ClassesRoot.OpenSubKey(Extension, true)?.DeleteSubKeyTree(Extension);
        Registry.ClassesRoot.OpenSubKey(KeyName, true)?.DeleteSubKeyTree(KeyName);
        return !IsAssociated(Extension, KeyName);
    }

    public static void AssociateExtension(string Extension, string Description, string KeyName)
    {
        Registry.SetValue($@"HKEY_CLASSES_ROOT\{Extension}",
            "", KeyName);
        Registry.SetValue($@"HKEY_CLASSES_ROOT\{Extension}\OpenWithList\RiftRipper.exe",
            "", "");
        Registry.SetValue($@"HKEY_CLASSES_ROOT\{Extension}\OpenWithProgids",
            KeyName, "");
        Registry.SetValue(@"HKEY_CLASSES_ROOT\Applications\RiftRipper.exe\Shell\Open\Command",
            "", $"\"{Application.ExecutablePath}\" Project=\"%1\"");
        Registry.SetValue($@"HKEY_CLASSES_ROOT\{KeyName}",
            "", Description);
        //Registry.SetValue(@"HKEY_CLASSES_ROOT\" + KeyName + @"\DefaultIcon", "", "");
        Registry.SetValue($@"HKEY_CLASSES_ROOT\{KeyName}\shell\Open\Command",
            "", $"\"{Application.ExecutablePath}\" Project=\"%1\"");


        /*
        RegistryKey BaseKey;
        RegistryKey OpenMethod;
        RegistryKey Shell;
        BaseKey = Registry.ClassesRoot.CreateSubKey(Extension);
        BaseKey.SetValue("", KeyName);

        OpenMethod = Registry.ClassesRoot.CreateSubKey(KeyName);
        OpenMethod.SetValue("", Description);
        OpenMethod.CreateSubKey("DefaultIcon").SetValue("", "\"" + Application.ExecutablePath + "\",0");
        Shell = OpenMethod.CreateSubKey("Shell");
        //Shell.CreateSubKey("edit").CreateSubKey("command").SetValue("", "\"" + Application.ExecutablePath + "\"" + " \"%1\"");
        Shell.CreateSubKey("open").CreateSubKey("command").SetValue("", "\"" + Application.ExecutablePath + "\"" + " \"%1\"");
        BaseKey.Close();
        OpenMethod.Close();
        Shell.Close();

        var CurrentUser = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + Extension, true);
        CurrentUser.DeleteSubKey("UserChoice", false);
        CurrentUser.Close();
        */

        SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
}
#endif