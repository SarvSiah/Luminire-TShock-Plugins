# Luminire.Essentials

Quality of life essentials for TShock 6.1.

## Features

- Join messages (customizable)
- `/clearinv`, `/ci`, `/lclear` - Clear inventory
- `/back`, `/lback` - Return to death location
- `/lhelp` - Help
- `/luminfo` - Plugin info

## Permissions

- `luminire.essentials.help`
- `luminire.essentials.clear`
- `luminire.essentials.clear.others`
- `luminire.essentials.back`
- `luminire.essentials.info`
- `luminire.admin` (bypass)

## Config

`tshock/Luminire/Essentials.json`

```json
{
  "enableJoinMessage": true,
  "joinMessageFormat": "{player} has joined Luminire!",
  "enableBackCommand": true,
  "enableClearCommand": true
}
```

## Building

```bash
dotnet build -c Release
```

DLL output: `bin/Release/net9.0/Luminire.Essentials.dll`

Requires `Luminire.Core.dll` in ServerPlugins.

## Compatibility

- TShock 6.1.0
- Terraria 1.4.5.6
- .NET 9.0
- OTAPI 3.3.11
- ApiVersion 2.1
