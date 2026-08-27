using TShockAPI;

namespace Luminire.Core;

public static class PlayerExtensions
{
    public static bool HasPermissionOrBypass(this TSPlayer player, string permission)
    {
        if (player == null) return false;
        return player.HasPermission(permission) || player.HasPermission("luminire.admin");
    }

    public static void SendSuccess(this TSPlayer player, string message)
        => player.SendSuccessMessage(message);

    public static void SendErrorMessageSafe(this TSPlayer player, string message)
        => player.SendErrorMessage(message);

    public static void SendInfoMessageSafe(this TSPlayer player, string message)
        => player.SendInfoMessage(message);
}

public static class CommandExtensions
{
    public static bool IsPlayerReal(this CommandArgs args)
        => args.Player != null && args.Player.RealPlayer && !args.Player.IsLoggedIn == false;
}
