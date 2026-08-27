# Luminire.Core

Shared core library for Luminire TShock plugins.

**Not a standalone plugin** - referenced by other plugins.

## Features

- `ConfigBase<T>` - JSON config with auto load/save
- `LuminireLog` - Logging wrapper
- `PlayerExtensions` - Player helper extensions
- `CommandExtensions` - Command helper extensions

## Usage

Reference in your plugin csproj:

```xml
<ProjectReference Include="..\Luminire.Core\Luminire.Core.csproj" Private="false" ExcludeAssets="runtime" />
```

Ensure `Luminire.Core.dll` is in `ServerPlugins/` alongside your plugin.

## Compatibility

- TShock 6.1.0
- Terraria 1.4.5.6
- .NET 9.0
