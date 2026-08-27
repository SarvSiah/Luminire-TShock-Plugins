using System.Diagnostics;

namespace Luminire.Core;

public static class LuminireLog
{
    public static void Info(string plugin, string message)
    {
        TShockAPI.TShock.Log.ConsoleInfo($"[{plugin}] {message}");
    }

    public static void Warn(string plugin, string message)
    {
        TShockAPI.TShock.Log.ConsoleWarn($"[{plugin}] {message}");
    }

    public static void Error(string plugin, string message)
    {
        TShockAPI.TShock.Log.ConsoleError($"[{plugin}] {message}");
    }

    [Conditional("DEBUG")]
    public static void Debug(string plugin, string message)
    {
        TShockAPI.TShock.Log.ConsoleDebug($"[{plugin}] {message}");
    }
}
