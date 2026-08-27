# Luminire TShock Plugins

> Professional, open-source plugin suite for **TShock 6.1.0** | **Terraria 1.4.5.6** | **.NET 9.0** | **OTAPI 3.3.11** | **TSAPI 2.1**

[![TShock 6.1.0](https://img.shields.io/badge/TShock-6.1.0-blue?style=for-the-badge)](https://github.com/Pryaxis/TShock/releases/tag/v6.1.0)
[![Terraria 1.4.5.6](https://img.shields.io/badge/Terraria-1.4.5.6-green?style=for-the-badge)](https://terraria.org)
[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-purple?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
[![License MIT](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](LICENSE)
[![Build](https://img.shields.io/github/actions/workflow/status/SarvSiah/Luminire-TShock-Plugins/build.yml?branch=main&label=Build&style=for-the-badge)](https://github.com/SarvSiah/Luminire-TShock-Plugins/actions)

Luminire is a curated collection of high-quality, performant, and maintainable TShock plugins built for the modern TShock 6.1 ecosystem. Designed with .NET 9 best practices, clean architecture, and community standards.

---

## ✨ Features

- ✅ **TShock 6.1 Ready** - Built against TShock 6.1.0, OTAPI 3.3.11, .NET 9.0, API 2.1
- 🧩 **Modular** - Each plugin is independent, use what you need
- 🛡️ **Safe** - Proper hook deregistration, config handling, permission checks
- 📦 **NuGet-based** - No binary dependencies committed, clean restore
- 🔧 **Maintainable** - Shared core library, editorconfig, analyzers
- 🚀 **CI/CD** - GitHub Actions for build, test, release

---

## 📋 Requirements

| Component | Version |
|-----------|---------|
| TShock | **6.1.0** (for Terraria 1.4.5.6) |
| Terraria | **1.4.5.6** |
| .NET Runtime | **9.0** |
| OTAPI | **3.3.11** |
| TerrariaServerAPI | **2.1** (ApiVersion) |

> **Important:** TShock 6.x uses .NET 9. Plugins built for .NET 6 (TShock 5.x) or .NET Framework 4.x (TShock 4.x) will **NOT** load on TShock 6.1. This repository targets only TShock 6.1+.

---

## 📦 Plugins

| Plugin | Description | Permissions | Version |
|--------|-------------|-------------|---------|
| **Luminire.Core** | Shared library - config base, logging, extensions (not a plugin, referenced by others) | - | 1.0.0 |
| **Luminire.Essentials** | QoL commands: `/clearinv`, `/back`, join messages, info | `luminire.essentials.*` | 1.0.0 |
| **Luminire.Template** | Starter template to create your own plugin - copy & rename | `luminire.template.*` | 1.0.0 |

### Coming Soon / Roadmap

- `Luminire.Economy` - Player economy & shops
- `Luminire.Ranks` - Playtime ranks & rewards
- `Luminire.Chat` - Advanced chat formatting & channels
- `Luminire.Protection` - Region enhancements
- `Luminire.Admin` - Admin tools, vanish, spy

Want a plugin? Open a [Feature Request](.github/ISSUE_TEMPLATE/feature_request.yml)!

---

## 🚀 Installation

1. **Download** the latest release from [Releases](https://github.com/SarvSiah/Luminire-TShock-Plugins/releases)
2. **Extract** the zip - you'll get `.dll` files
3. **Copy** the `.dll` you want into your server's `ServerPlugins/` folder
   ```
   TShock/
   ├── TerrariaServer.exe
   ├── ServerPlugins/
   │   ├── TShockAPI.dll (existing)
   │   ├── Luminire.Core.dll
   │   ├── Luminire.Essentials.dll  <- drop here
   │   └── ...
   └── tshock/
   ```
4. **Restart** your server
5. **Check console** - you should see `[Luminire.Essentials] v1.0.0 initialized`
6. **Configure** - Edit `tshock/Luminire/Essentials.json` then `/reload`

---

## 🔨 Building from Source

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) (9.0.100+)
- Git

### Build

```bash
git clone https://github.com/SarvSiah/Luminire-TShock-Plugins.git
cd Luminire-TShock-Plugins

# Restore & Build Release
dotnet build -c Release

# Output DLLs will be in:
# src/Luminire.Essentials/bin/Release/net9.0/Luminire.Essentials.dll
# src/Luminire.Core/bin/Release/net9.0/Luminire.Core.dll
```

### Create New Plugin

1. Copy `src/Luminire.Template` to `src/Luminire.YourName`
2. Rename `.csproj`, namespace, and `Plugin.cs` class
3. Update `Name`, `Author`, `Description`, `Version` in Plugin.cs
4. Add to solution: `dotnet sln add src/Luminire.YourName/Luminire.YourName.csproj`
5. Build!

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed guide.

---

## ⚙️ Configuration

Configs are stored in `tshock/Luminire/` as JSON:

**Essentials.json**
```json
{
  "enableJoinMessage": true,
  "joinMessageFormat": "{player} has joined Luminire!",
  "enableDiscordLogging": false,
  "maxHomesPerPlayer": 5,
  "enableHomeCommand": true,
  "enableBackCommand": true,
  "enableClearCommand": true
}
```

Reload with `/reload` in-game.

---

## 🔐 Permissions

| Permission | Description | Default Group |
|------------|-------------|---------------|
| `luminire.essentials.help` | Use /lhelp | guest |
| `luminire.essentials.clear` | Clear own inventory | registered |
| `luminire.essentials.clear.others` | Clear others inventory | admin |
| `luminire.essentials.back` | Use /back command | registered |
| `luminire.essentials.info` | View plugin info | guest |
| `luminire.template.use` | Use template commands | guest |
| `luminire.template.admin` | Use admin template commands | admin |
| `luminire.admin` | Bypass all Luminire checks | superadmin |

Add permissions via `/group addperm <group> <permission>`

---

## 🤝 Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

- Fork the repo
- Create feature branch (`git checkout -b feature/amazing-feature`)
- Commit (`git commit -m 'Add amazing feature'`)
- Push (`git push origin feature/amazing-feature`)
- Open Pull Request

---

## 📚 Documentation

- [TShock Documentation](https://tshock.readme.io/)
- [TShock 6.1 Release Notes](https://github.com/Pryaxis/TShock/releases/tag/v6.1.0)
- [TShock Plugin Guide](https://tshock.readme.io/docs/hello-world)
- [Terraria Wiki](https://terraria.wiki.gg/wiki/TShock)

---

## 🛡️ Security

See [SECURITY.md](SECURITY.md) for reporting vulnerabilities.

---

## 📄 License

MIT License - see [LICENSE](LICENSE)

---

## 🙏 Credits

- [Pryaxis / TShock Team](https://github.com/Pryaxis/TShock) - For TShock, TSAPI, OTAPI
- [Terraria](https://terraria.org/) - Re-Logic
- [SarvSiah](https://github.com/SarvSiah) - Luminire founder & maintainer
- Community contributors

---

## 💬 Support

- **Issues:** [GitHub Issues](https://github.com/SarvSiah/Luminire-TShock-Plugins/issues)
- **Discussions:** [GitHub Discussions](https://github.com/SarvSiah/Luminire-TShock-Plugins/discussions)
- **Discord:** Coming soon!

**If you like this project, please ⭐ star the repo!**

---

### 🏷️ Compatibility Matrix

| Luminire Version | TShock Version | Terraria | .NET | Status |
|-----------------|----------------|----------|------|--------|
| 1.0.x | 6.1.0 | 1.4.5.6 | 9.0 | ✅ Active |
| - | 6.0.0 | 1.4.5.5 | 9.0 | ⚠️ Compatible (rebuild) |
| - | 5.2.x | 1.4.4.9 | 6.0 | ❌ Not supported |

---

*Built with ❤️ for the Terraria community*
