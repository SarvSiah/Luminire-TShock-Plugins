using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Luminire.Core;

namespace Luminire.Essentials;

[ApiVersion(2, 1)]
public class EssentialsPlugin : TerrariaPlugin
{
    public override string Name => "Luminire.Essentials";
    public override string Author => "SarvSiah & Luminire Team";
    public override string Description => "Quality of life essentials for TShock 6.1 - homes, back, clear, join messages.";
    public override Version Version => Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0);

    public static EssentialsConfig Config { get; private set; } = new();

    // Simple in-memory back positions
    private readonly Dictionary<string, (int x, int y)> _lastPositions = new();

    public EssentialsPlugin(Main game) : base(game)
    {
        Order = 1;
    }

    public override void Initialize()
    {
        Config = EssentialsConfig.Load();

        // Commands
        Commands.ChatCommands.Add(new Command("luminire.essentials.help", HelpCmd, "lhelp", "luminire"));
        Commands.ChatCommands.Add(new Command("luminire.essentials.clear", ClearCmd, "clearinv", "ci", "lclear")
        {
            HelpText = "Clears your inventory or specified player's inventory."
        });
        Commands.ChatCommands.Add(new Command("luminire.essentials.back", BackCmd, "back", "lback")
        {
            HelpText = "Teleport back to your death location or last teleport.",
            AllowServer = false
        });
        Commands.ChatCommands.Add(new Command("luminire.essentials.info", InfoCmd, "luminfo", "lversion")
        {
            HelpText = "Shows Luminire plugin info."
        });

        // Hooks
        ServerApi.Hooks.NetGreetPlayer.Register(this, OnGreetPlayer);
        ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
        TShockAPI.Hooks.PlayerHooks.PlayerPostLogin += OnPostLogin;
        GetDataHandlers.KillMe += OnKillMe;

        LuminireLog.Info(Name, $"v{Version} initialized. TShock 6.1 (Terraria 1.4.5.6 / .NET 9.0) ready.");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ServerApi.Hooks.NetGreetPlayer.Deregister(this, OnGreetPlayer);
            ServerApi.Hooks.ServerLeave.Deregister(this, OnLeave);
            TShockAPI.Hooks.PlayerHooks.PlayerPostLogin -= OnPostLogin;
            GetDataHandlers.KillMe -= OnKillMe;

            var asm = Assembly.GetExecutingAssembly();
            Commands.ChatCommands.RemoveAll(c => c.CommandDelegate.Method?.DeclaringType?.Assembly == asm);
        }
        base.Dispose(disposing);
    }

    #region Hooks

    private void OnGreetPlayer(GreetPlayerEventArgs args)
    {
        if (!Config.EnableJoinMessage) return;
        var player = TShock.Players[args.Who];
        if (player == null) return;

        // Delay slightly to ensure name is available
        Task.Delay(500).ContinueWith(_ =>
        {
            var msg = Config.JoinMessageFormat.Replace("{player}", player.Name);
            TShock.Utils.Broadcast(msg, Color.MediumPurple);
        });
    }

    private void OnLeave(LeaveEventArgs args)
    {
        var player = TShock.Players[args.Who];
        if (player != null)
        {
            _lastPositions.Remove(player.Name);
        }
    }

    private void OnPostLogin(TShockAPI.Hooks.PlayerPostLoginEventArgs args)
    {
        // Example: welcome back message
        args.Player?.SendInfoMessage($"Welcome back, {args.Player.Name}! This server runs {Name} v{Version} on TShock 6.1.0");
    }

    private void OnKillMe(object? sender, GetDataHandlers.KillMeEventArgs args)
    {
        if (!Config.EnableBackCommand) return;
        var player = args.Player;
        if (player == null || !player.RealPlayer) return;

        // Save death position for /back
        _lastPositions[player.Name] = (player.TPlayer.position.X.ToInt(), player.TPlayer.position.Y.ToInt());
        player.SendInfoMessage("You died! Use /back to return to your death point.");
    }

    #endregion

    #region Commands

    private void HelpCmd(CommandArgs args)
    {
        args.Player.SendInfoMessage("[Luminire Essentials]");
        args.Player.SendInfoMessage("/lhelp - This help");
        if (Config.EnableClearCommand) args.Player.SendInfoMessage("/clearinv, /ci, /lclear [player] - Clear inventory");
        if (Config.EnableBackCommand) args.Player.SendInfoMessage("/back, /lback - Return to death location");
        args.Player.SendInfoMessage("/luminfo - Plugin info");
        args.Player.SendInfoMessage($"Running TShock {TShock.VersionNum} for Terraria {Main.versionNumber} (API {ApiVersion})");
    }

    private void InfoCmd(CommandArgs args)
    {
        args.Player.SendSuccessMessage($"[Luminire] {Name} v{Version}");
        args.Player.SendInfoMessage($"Author: {Author}");
        args.Player.SendInfoMessage($"Description: {Description}");
        args.Player.SendInfoMessage($"TShock: {TShock.VersionNum} | Terraria: {Main.versionNumber} | OTAPI: OTAPI 3.3.11+ | .NET 9.0");
        args.Player.SendInfoMessage($"Config: {Config.ConfigPath}");
    }

    private void ClearCmd(CommandArgs args)
    {
        if (!Config.EnableClearCommand)
        {
            args.Player.SendErrorMessage("Clear command is disabled in config.");
            return;
        }

        TSPlayer target = args.Player;
        if (args.Parameters.Count > 0)
        {
            if (!args.Player.HasPermission("luminire.essentials.clear.others"))
            {
                args.Player.SendErrorMessage("You don't have permission to clear other players.");
                return;
            }

            var found = TSPlayer.FindByNameOrID(args.Parameters[0]);
            if (found.Count == 0)
            {
                args.Player.SendErrorMessage($"Player '{args.Parameters[0]}' not found.");
                return;
            }
            if (found.Count > 1)
            {
                args.Player.SendMultipleMatchError(found.Select(p => p.Name));
                return;
            }
            target = found[0];
        }

        if (!target.RealPlayer)
        {
            args.Player.SendErrorMessage("Target player not online or not real.");
            return;
        }

        // Clear inventory (0-58 = inventory, plus armor etc handled by TShock?)
        for (int i = 0; i < NetItem.InventorySlots; i++)
        {
            target.TPlayer.inventory[i].TurnToAir();
        }
        for (int i = 0; i < NetItem.ArmorSlots; i++)
        {
            target.TPlayer.armor[i].TurnToAir();
        }
        for (int i = 0; i < NetItem.DyeSlots; i++)
        {
            target.TPlayer.dye[i].TurnToAir();
        }
        target.TPlayer.trashItem.TurnToAir();

        target.SendData(PacketTypes.PlayerSlot, "", target.Index, 0, 0);
        target.SendData(PacketTypes.PlayerSlot, "", target.Index, 1, 0);
        // Full sync
        TSPlayer.All.SendData(PacketTypes.PlayerSlot, "", target.Index);

        if (target == args.Player)
            args.Player.SendSuccessMessage("Your inventory has been cleared.");
        else
        {
            args.Player.SendSuccessMessage($"Cleared {target.Name}'s inventory.");
            target.SendInfoMessage($"{args.Player.Name} cleared your inventory.");
        }

        LuminireLog.Info(Name, $"{args.Player.Name} cleared inventory of {target.Name}");
    }

    private void BackCmd(CommandArgs args)
    {
        if (!Config.EnableBackCommand)
        {
            args.Player.SendErrorMessage("Back command is disabled.");
            return;
        }

        if (!_lastPositions.TryGetValue(args.Player.Name, out var pos))
        {
            args.Player.SendErrorMessage("No previous position found. You need to die first!");
            return;
        }

        if (args.Player.Teleport(pos.x, pos.y))
        {
            args.Player.SendSuccessMessage($"Teleported back to death point ({pos.x / 16}, {pos.y / 16}).");
            _lastPositions.Remove(args.Player.Name);
        }
        else
        {
            args.Player.SendErrorMessage("Teleport failed.");
        }
    }

    #endregion
}

internal static class IntExtensions
{
    public static int ToInt(this float f) => (int)f;
}
