# Compatibility

## TShock Version Support

| Luminire Version | TShock Version | Terraria Version | .NET Version | OTAPI Version | TSAPI ApiVersion | Status |
|------------------|----------------|------------------|--------------|---------------|------------------|--------|
| 1.0.x            | 6.1.0          | 1.4.5.6          | 9.0          | 3.3.11        | 2.1              | ✅ Supported |
| -                | 6.0.0          | 1.4.5.5          | 9.0          | 3.3.10        | 2.1              | ⚠️ Compatible (rebuild recommended) |
| -                | 5.2.4          | 1.4.4.9          | 6.0          | 3.1.x         | 2.1              | ❌ Not supported |
| -                | 4.5.x          | 1.4.3.x          | net472       | 2.x           | 2.1              | ❌ Not supported |

## .NET Runtimes

TShock has changed runtime twice:

- **TShock 4.x**: .NET Framework 4.7.2 / net472
- **TShock 5.x**: .NET 6.0
- **TShock 6.x**: .NET 9.0 (current)

Plugins built for wrong runtime will silently fail to load (no error in console, just not appearing). Always check `<TargetFramework>` in `.csproj`:

- `net9.0` => TShock 6.x ✅ (this repo)
- `net6.0` => TShock 5.x ❌
- `net472` or `netstandard2.0` => TShock 4.x ❌

## Terraria Protocol

Terraria 1.4.5.6 uses protocol version that requires OTAPI 3.3.11. Older OTAPI versions will not work.

## How to Check Your Plugin

Open your `.csproj`:

```xml
<TargetFramework>net9.0</TargetFramework>
<PackageReference Include="TShock" Version="6.1.0" />
```

And plugin class:

```csharp
[ApiVersion(2, 1)]
public class MyPlugin : TerrariaPlugin
```

If you see `net6.0` or TShock 5.2.x, you need to upgrade.

## Upgrading from TShock 5.x to 6.1

1. Update SDK: install .NET 9.0 SDK
2. Update csproj:
   - `<TargetFramework>net9.0</TargetFramework>`
   - `<PackageReference Include="TShock" Version="6.1.0" />`
3. Update code for breaking changes:
   - `Item.active` => `!Item.IsAir` or `Item.active` still exists? Check Terraria 1.4.5.6 API - `IsAir` is preferred
   - OTAPI hooks: `On.Terraria.*` static hooks signature changes
   - Nullable reference types: add null checks
4. Build and test on TShock 6.1.0 server
5. Update README compatibility table

## Docker

If using Docker:

- `ghcr.io/pryaxis/tshock:stable` => TShock 6.1.0 (latest stable)
- `ghcr.io/pryaxis/tshock:v6.1.0` => pinned to 6.1.0

Both require plugins built for net9.0.

## Supported Platforms

TShock 6.1.0 supports:

- Windows x64
- Linux x64
- Linux ARM64
- macOS x64 / ARM64

Luminire plugins are platform-agnostic (AnyCPU) and work on all platforms where TShock runs.

## Future .NET 10

TShock team announced work on .NET 10 likely before end of March 2026. When TShock moves to .NET 10:

- Luminire will create `net10.0` branch
- Maintain `net9.0` branch for 6.1 support
- Update this doc

Stay tuned to https://github.com/Pryaxis/TShock for announcements.
