# Development Guide - Luminire TShock Plugins

## TShock 6.1 Specifics

### Runtime

- **.NET 9.0** is required. TShock 6.0+ moved from .NET 6 to .NET 9.
- Your `.csproj` must have `<TargetFramework>net9.0</TargetFramework>`
- Use `dotnet` CLI 9.0.100+

### API Version

- TShock 6.1 still uses `ApiVersion(2, 1)` - this is TSAPI version, not TShock version.
- Keep `[ApiVersion(2, 1)]` on your plugin class.

### NuGet

Use TShock 6.1.0 package:

```xml
<PackageReference Include="TShock" Version="6.1.0" />
```

This transitively includes:

- OTAPI 3.3.11
- TSAPI 6.1.0
- TerrariaServer dependencies

No need to manually reference OTAPI.dll or TerrariaServer.dll anymore.

### Breaking Changes from TShock 5.x to 6.x

1. **.NET 9** - All plugins must recompile
2. **OTAPI static hook changes** - If you used OTAPI hooks directly (e.g., `On.Terraria.*`), signatures changed. Prefer TSAPI hooks.
3. **Item.active -> IsAir** - Terraria 1.4.5.x changed item handling. Use `item.IsAir` instead of `!item.active` where applicable.
4. **Nullable enabled** - Recommended to enable nullable and handle nulls.

## Project Structure

```
src/
  Luminire.Core/          # Shared utilities (not a plugin)
  Luminire.Essentials/    # Example functional plugin
  Luminire.Template/      # Template for new plugins
```

### Adding a New Plugin

1. Copy Template:
   ```bash
   cp -r src/Luminire.Template src/Luminire.MyPlugin
   cd src/Luminire.MyPlugin
   rm -rf bin obj
   ```

2. Rename `.csproj` and edit:
   - `<AssemblyName>Luminire.MyPlugin</AssemblyName>`
   - `<RootNamespace>Luminire.MyPlugin</RootNamespace>`

3. Rename namespace in all `.cs` files.

4. Edit `Plugin.cs`:
   ```csharp
   public override string Name => "Luminire.MyPlugin";
   public override string Author => "Your Name";
   public override string Description => "...";
   public override Version Version => new Version(1, 0, 0);
   ```

5. Add to solution:
   ```bash
   dotnet sln add src/Luminire.MyPlugin/Luminire.MyPlugin.csproj
   ```

6. Build:
   ```bash
   dotnet build -c Release
   ```

## Config Handling

Use `Luminire.Core.ConfigBase<T>`:

```csharp
public class MyConfig : ConfigBase<MyConfig>
{
    public bool EnableFeature { get; set; } = true;
    public override string ConfigPath => Path.Combine(TShock.SavePath, "Luminire", "MyPlugin.json");
}

// Load
Config = MyConfig.Load();

// Save
Config.Save();

// Reload handler
TShockAPI.Hooks.GeneralHooks.ReloadEvent += (args) => {
    Config = MyConfig.Load();
    args.Player.SendSuccessMessage("Config reloaded");
};
```

## Commands

```csharp
Commands.ChatCommands.Add(new Command("myplugin.use", MyCommand, "mycommand", "mc")
{
    HelpText = "Description shown in /help",
    AllowServer = true, // allow server console?
    DoLog = true
});

private void MyCommand(CommandArgs args)
{
    if (args.Player == null) return; // server console?
    args.Player.SendSuccessMessage("Hello!");
}
```

Permissions: always use `luminire.<plugin>.<action>` pattern. Provide `luminire.admin` bypass.

## Hooks

### TSAPI Hooks

```csharp
ServerApi.Hooks.NetGreetPlayer.Register(this, OnGreet);
ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
ServerApi.Hooks.GamePostInitialize.Register(this, OnPostInit);
```

Deregister in Dispose!

### TShock Hooks

```csharp
TShockAPI.Hooks.PlayerHooks.PlayerPostLogin += OnPostLogin;
TShockAPI.Hooks.GeneralHooks.ReloadEvent += OnReload;
TShockAPI.Hooks.AccountHooks.AccountCreate += OnAccountCreate;
```

### GetDataHandlers (Packet hooks)

```csharp
TShockAPI.GetDataHandlers.KillMe += OnKillMe;
TShockAPI.GetDataHandlers.PlayerUpdate += OnPlayerUpdate;
```

## Best Practices

- **Never** block main thread with `Thread.Sleep` or long loops
- **Always** check `player.RealPlayer` before accessing `TPlayer`
- **Always** deregister hooks in Dispose
- **Always** remove commands added by your assembly in Dispose
- Use `TSPlayer.FindByNameOrID` for player lookup
- Use `TShock.Utils.Broadcast` for broadcasts
- Use `TShock.Log` for logging, or `Luminire.Core.LuminireLog`
- Test with `/reload` - ensure no duplicate commands/hooks

## Debugging

1. Build Debug config: `dotnet build -c Debug`
2. Copy `.dll` and `.pdb` to `ServerPlugins/`
3. Start TShock with debugger attached (VS: Debug -> Attach to Process -> TerrariaServer)
4. Or use logging: `TShock.Log.ConsoleDebug`

## Releasing

Tag and push:

```bash
git tag v1.0.0
git push origin v1.0.0
```

GitHub Action will build and create release zip.

## Useful Links

- TShock Docs: https://tshock.readme.io/
- TShock GitHub: https://github.com/Pryaxis/TShock
- OTAPI: https://github.com/Pryaxis/OTAPI
- Terraria Wiki: https://terraria.wiki.gg/wiki/Server
