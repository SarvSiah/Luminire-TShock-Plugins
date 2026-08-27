# src/

Source code for Luminire TShock Plugins.

## Structure

```
src/
├── Luminire.Core/         # Shared library (not a plugin, but required)
│   ├── Luminire.Core.csproj
│   ├── ConfigBase.cs      # JSON config base
│   ├── Logging.cs         # Logging wrapper
│   └── Extensions.cs      # Player/Command extensions
│
├── Luminire.Essentials/   # Essentials plugin
│   ├── Luminire.Essentials.csproj
│   ├── Configuration.cs   # Essentials config
│   └── Plugin.cs          # Main plugin class
│
└── Luminire.Template/     # Template for new plugins
    ├── Luminire.Template.csproj
    ├── Configuration.cs
    └── Plugin.cs
```

## Building

```bash
dotnet build -c Release
```

Output DLLs: `src/<Plugin>/bin/Release/net9.0/*.dll`

## Adding New Plugin

1. Copy `Luminire.Template` to `Luminire.YourPlugin`
2. Rename csproj and namespace
3. Add to solution: `dotnet sln add src/Luminire.YourPlugin/Luminire.YourPlugin.csproj`
4. Build

See `docs/DEVELOPMENT.md` for details.

## Dependencies

All plugins target:

- `net9.0`
- `TShock` 6.1.0 (includes TSAPI 6.1.0, OTAPI 3.3.11)

No binary DLLs committed - NuGet restore only.

## Shared Library

`Luminire.Core` is referenced by all plugins:

```xml
<ProjectReference Include="..\Luminire.Core\Luminire.Core.csproj" Private="false" ExcludeAssets="runtime" />
```

Ensure `Luminire.Core.dll` is in `ServerPlugins/` alongside other plugins.

## Compatibility

- TShock 6.1.0
- Terraria 1.4.5.6
- .NET 9.0
- ApiVersion 2.1
