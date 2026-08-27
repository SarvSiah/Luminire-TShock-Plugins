# Installation Guide

## Prerequisites

- TShock 6.1.0 for Terraria 1.4.5.6
  - Download from https://github.com/Pryaxis/TShock/releases/tag/v6.1.0
- .NET 9.0 Runtime (if using TShock standalone)
  - Download from https://dotnet.microsoft.com/en-us/download/dotnet/9.0

Check your TShock version on server start:
```
[Server API] Info API Version: 2.1
TShock 6.1.0.0 (1.4.5.6) now running.
```

If you see TShock 5.x, you need to upgrade TShock first.

## Installing Luminire Plugins

### Method 1: Release Zip (Recommended)

1. Go to https://github.com/SarvSiah/Luminire-TShock-Plugins/releases
2. Download latest `Luminire-TShock-Plugins-vX.X.X-net9.0-TShock6.1.0.zip`
3. Extract
4. Copy `ServerPlugins/*.dll` to your server's `ServerPlugins/` folder
   - Keep `Luminire.Core.dll` if present - it's required by other plugins
5. Restart server

### Method 2: Individual DLL

Download specific DLL from release assets and drop into `ServerPlugins/`.

### Folder Structure After Install

```
TerrariaServer/
├── TerrariaServer.exe (or TShock.Server)
├── ServerPlugins/
│   ├── TShockAPI.dll (from TShock)
│   ├── BCrypt.Net.dll
│   ├── ...
│   ├── Luminire.Core.dll
│   ├── Luminire.Essentials.dll
│   └── Luminire.Template.dll (optional)
├── tshock/
│   ├── config.json
│   ├── tshock.sqlite
│   └── Luminire/
│       ├── Essentials.json (auto-created)
│       └── Template.json
└── worlds/
```

## Verifying Installation

On server start, you should see:

```
[Luminire.Essentials] v1.0.0 initialized. TShock 6.1 (Terraria 1.4.5.6 / .NET 9.0) ready.
[Luminire.Template] Initialized v1.0.0 - Hello from Luminire Template!
```

In-game, try:

- `/lhelp` - Essentials help
- `/luminfo` - Plugin info
- `/template` - Template command

If plugin doesn't load:

- Check `tshock/logs/` for errors
- Ensure you have .NET 9.0: `dotnet --info`
- Ensure TShock is 6.1.0, not 5.x
- Ensure DLLs are not blocked (Windows: right-click -> Properties -> Unblock)

## Configuration

Configs are auto-created in `tshock/Luminire/`:

- `Essentials.json`
- `Template.json`

Edit with any text editor, then `/reload` in-game.

## Permissions

Use TShock commands to set permissions:

```
/group addperm default luminire.essentials.help
/group addperm default luminire.essentials.back
/group addperm default luminire.essentials.info
/group addperm vip luminire.essentials.clear
/group addperm admin luminire.essentials.clear.others
/group addperm admin luminire.template.admin
/group addperm superadmin luminire.admin
```

See main README for full permission list.

## Updating

1. Stop server
2. Backup `ServerPlugins/` and `tshock/Luminire/`
3. Replace old DLLs with new ones
4. Start server
5. Check logs

Configs are preserved, but check for new options.

## Uninstalling

1. Stop server
2. Delete plugin DLL from `ServerPlugins/`
3. (Optional) Delete config from `tshock/Luminire/`
4. Start server

## Docker

If using `ghcr.io/pryaxis/tshock:stable`:

```bash
docker run -d \
  --name tshock \
  -p 7777:7777 \
  -v ./tshock:/tshock \
  -v ./worlds:/worlds \
  -v ./plugins:/plugins \
  ghcr.io/pryaxis/tshock:stable

# Then copy DLLs to ./plugins/ which maps to /plugins inside container
# Actually TShock container expects ServerPlugins in /tshock/ServerPlugins? Check docs
# For official image, plugins go to /plugins and are copied on start
```

Check https://github.com/Pryaxis/TShock for Docker details.

## Troubleshooting

### Plugin not loading, no message in console

- Wrong .NET version. TShock 6.1 needs .NET 9.0
- Check `dotnet --list-runtimes` includes `Microsoft.NETCore.App 9.0.x`
- Ensure DLL targets `net9.0`: open .csproj, check `<TargetFramework>`

### Config not created

- Check `tshock/` folder permissions (write access)
- Look in server root for `tshock/Luminire/` creation errors in logs

### Commands not showing

- Permission missing
- Check `/help` shows commands only if you have permission
- Try as superadmin or `/group addperm`

### Server crash on load

- Check `tshock/logs/` latest log
- Ensure only one version of each plugin DLL
- Remove other outdated plugins that may conflict
- Open issue with logs

## Support

- Issues: https://github.com/SarvSiah/Luminire-TShock-Plugins/issues
- Discussions: https://github.com/SarvSiah/Luminire-TShock-Plugins/discussions
