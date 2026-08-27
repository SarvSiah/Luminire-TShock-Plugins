# API Reference - Luminire.Core

Shared library for all Luminire plugins.

## ConfigBase<T>

Base class for JSON configs.

```csharp
public class MyConfig : ConfigBase<MyConfig>
{
    public bool EnableFeature { get; set; } = true;
    public override string ConfigPath => Path.Combine(TShock.SavePath, "Luminire", "MyPlugin.json");
}

// Usage
var config = MyConfig.Load();
config.EnableFeature = false;
config.Save();
```

Features:

- Auto-creates file if missing
- JSON indented, camelCase
- Error handling with console logging
- Override ConfigPath for custom location

## LuminireLog

Wrapper around TShock.Log:

```csharp
LuminireLog.Info("MyPlugin", "Hello");
LuminireLog.Warn("MyPlugin", "Warning");
LuminireLog.Error("MyPlugin", "Error");
LuminireLog.Debug("MyPlugin", "Debug (only in DEBUG builds)");
```

## PlayerExtensions

```csharp
player.HasPermissionOrBypass("myplugin.use"); // checks luminire.admin bypass
player.SendSuccess("Message");
player.SendErrorMessageSafe("Error");
player.SendInfoMessageSafe("Info");
```

## CommandExtensions

```csharp
args.IsPlayerReal(); // checks RealPlayer and LoggedIn
```

## Adding More Shared Code

Place shared utilities in `Luminire.Core` and reference from other plugins:

```xml
<ProjectReference Include="..\Luminire.Core\Luminire.Core.csproj" Private="false" ExcludeAssets="runtime" />
```

Use `Private="false" ExcludeAssets="runtime"` so Core DLL is not copied as private? Actually we want Core DLL to be present. Adjust as needed.

## Versioning

Core follows same version as main repo. Breaking changes will bump major version.

## Future APIs Planned

- Database helper (SQLite/MySQL)
- Discord webhook helper
- Player data persistence
- Localization helper
```

