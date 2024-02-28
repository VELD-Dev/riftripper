using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiftRipper.Utility;

internal static class ErrorHandler
{
    public static void Alert(string message, bool textWrapped = true)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
        Window.Singleton.AddFrame(new ErrorAlertModal(Window.Singleton, message, textWrapped));
    }

    public static void Alert(Exception ex, bool textWrapped = false)
    {
        Alert(ex.ToString(), textWrapped);
    }
}
