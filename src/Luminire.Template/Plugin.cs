using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Luminire.Core;

namespace Luminire.Template;

/// <summary>
/// Luminire Template Plugin - Copy this to create your own plugin.
/// Compatible with TShock 6.1.0 / Terraria 1.4.5.6 / .NET 9.0 / OTAPI 3.3.11 / ApiVersion 2.1
/// </summary>
[ApiVersion(2, 1)]
public class TemplatePlugin : TerrariaPlugin
{
    public override string Name => "Luminire.Template";
    public override string Author => "SarvSiah & Luminire Team";
    public override string Description => "Template plugin for Luminire TShock 6.1 - copy me to start your own plugin!";
    public override Version Version => Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0);

    public static TemplateConfig Config { get; private set; } = new();

    public TemplatePlugin(Main game) : base(game)
    {
        // Order defines load order - lower loads first
        Order = 10;
    }

    public override void Initialize()
    {
        // Load config
        Config = TemplateConfig.Load();

        // Register commands
        // Format: new Command(permission, commandDelegate, alias1, alias2, ...)
        Commands.ChatCommands.Add(new Command("luminire.template.use", TemplateCommand, "template", "ltemplate")
        {
            HelpText = "Example command from template plugin."
        });

        Commands.ChatCommands.Add(new Command("luminire.template.admin", AdminCommand, "tadmin")
        {
            HelpText = "Admin example command."
        });

        // Register hooks
        // TSAPI hooks
        ServerApi.Hooks.GameInitialize.Register(this, OnGameInitialize);
        ServerApi.Hooks.NetGreetPlayer.Register(this, OnGreetPlayer);

        // TShock hooks
        TShockAPI.Hooks.GeneralHooks.ReloadEvent += OnReload;
        TShockAPI.Hooks.PlayerHooks.PlayerPostLogin += OnPostLogin;

        // GetDataHandlers example (packet handling)
        // TShockAPI.GetDataHandlers.PlayerUpdate += OnPlayerUpdate;

        LuminireLog.Info(Name, $"Initialized v{Version} - {Config.ExampleMessage}");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Deregister hooks
            ServerApi.Hooks.GameInitialize.Deregister(this, OnGameInitialize);
            ServerApi.Hooks.NetGreetPlayer.Deregister(this, OnGreetPlayer);

            TShockAPI.Hooks.GeneralHooks.ReloadEvent -= OnReload;
            TShockAPI.Hooks.PlayerHooks.PlayerPostLogin -= OnPostLogin;
            // TShockAPI.GetDataHandlers.PlayerUpdate -= OnPlayerUpdate;

            // Remove commands added by this assembly
            var asm = Assembly.GetExecutingAssembly();
            Commands.ChatCommands.RemoveAll(c => c.CommandDelegate.Method?.DeclaringType?.Assembly == asm);
        }
        base.Dispose(disposing);
    }

    #region Hooks

    private void OnGameInitialize(EventArgs args)
    {
        // Called when server is initialized
        LuminireLog.Info(Name, "GameInitialize hook fired.");
    }

    private void OnGreetPlayer(GreetPlayerEventArgs args)
    {
        if (!Config.ExampleFeatureEnabled) return;

        var player = TShock.Players[args.Who];
        // Example: send message on join
        // player?.SendInfoMessage(Config.ExampleMessage);
    }

    private void OnReload(TShockAPI.Hooks.ReloadEventArgs args)
    {
        Config = TemplateConfig.Load();
        args.Player.SendSuccessMessage($"[{Name}] Config reloaded.");
        LuminireLog.Info(Name, $"Config reloaded by {args.Player.Name}");
    }

    private void OnPostLogin(TShockAPI.Hooks.PlayerPostLoginEventArgs args)
    {
        // Example: actions after login
        LuminireLog.Debug(Name, $"{args.Player.Name} logged in.");
    }

    #endregion

    #region Commands

    private void TemplateCommand(CommandArgs args)
    {
        if (!Config.ExampleFeatureEnabled)
        {
            args.Player.SendErrorMessage("This feature is disabled in config.");
            return;
        }

        if (args.Parameters.Count == 0)
        {
            args.Player.SendInfoMessage($"[Template] {Config.ExampleMessage}");
            args.Player.SendInfoMessage($"Example number is {Config.ExampleNumber}");
            args.Player.SendInfoMessage("Usage: /template <message>");
            return;
        }

        var message = string.Join(" ", args.Parameters);
        args.Player.SendSuccessMessage($"You said: {message}");
        TShock.Utils.Broadcast($"[Template] {args.Player.Name}: {message}", Color.LightGreen);
    }

    private void AdminCommand(CommandArgs args)
    {
        args.Player.SendSuccessMessage($"[Template] Admin command executed by {args.Player.Name}");
        args.Player.SendInfoMessage($"Server TPS: {TShockAPI.TShock.Config.Settings.InfiniteInvasion} | Players: {TShock.Utils.GetActivePlayerCount()}/{TShock.Config.Settings.MaxSlots}");
        args.Player.SendInfoMessage($"Config path: {Config.ConfigPath}");
    }

    #endregion
}
