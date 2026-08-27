# Contributing to Luminire TShock Plugins

Thank you for considering contributing! This document provides guidelines to make contributing easy and effective.

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [Creating a New Plugin](#creating-a-new-plugin)
- [Coding Standards](#coding-standards)
- [Commit Messages](#commit-messages)
- [Pull Request Process](#pull-request-process)
- [Reporting Bugs](#reporting-bugs)
- [Feature Requests](#feature-requests)

## Code of Conduct

Please read and follow our [Code of Conduct](CODE_OF_CONDUCT.md).

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR_USERNAME/Luminire-TShock-Plugins.git`
3. Create a branch: `git checkout -b feature/your-feature-name`

## Development Setup

### Requirements

- .NET 9.0 SDK (9.0.100 or later)
- IDE: Visual Studio 2022 17.12+, Rider, or VS Code with C# Dev Kit
- TShock 6.1.0 server for testing (optional but recommended)

### Build

```bash
dotnet restore
dotnet build -c Release
```

### Testing Locally

1. Build plugins in Release
2. Copy DLLs to your local TShock `ServerPlugins/` folder
3. Start TShock server and verify logs
4. Test commands in-game

## Creating a New Plugin

### Quick Start (Copy Template)

```bash
# Copy template
cp -r src/Luminire.Template src/Luminire.YourPlugin

# Rename files and namespace
# 1. Rename Luminire.Template.csproj -> Luminire.YourPlugin.csproj
# 2. Edit Plugin.cs: change namespace, class name, Name, Description, Version
# 3. Add to solution
dotnet sln add src/Luminire.YourPlugin/Luminire.YourPlugin.csproj
```

### Plugin Structure Checklist

Your plugin MUST:

- [ ] Target `net9.0`
- [ ] Reference `TShock` 6.1.0 via NuGet (not DLL)
- [ ] Use `[ApiVersion(2, 1)]`
- [ ] Inherit from `TerrariaPlugin`
- [ ] Override `Name`, `Author`, `Description`, `Version`
- [ ] Implement `Initialize()` and `Dispose(bool disposing)`
- [ ] Deregister all hooks and commands in `Dispose`
- [ ] Use `Luminire.Core` for shared logic if applicable
- [ ] Include permission checks for commands
- [ ] Handle config load/save safely
- [ ] Not use `async void` except for event handlers
- [ ] Follow EditorConfig formatting

### Example Plugin Skeleton

```csharp
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Luminire.YourPlugin;

[ApiVersion(2, 1)]
public class YourPlugin : TerrariaPlugin
{
    public override string Name => "Luminire.YourPlugin";
    public override string Author => "YourName";
    public override string Description => "What your plugin does";
    public override Version Version => new Version(1, 0, 0);

    public YourPlugin(Main game) : base(game) { Order = 1; }

    public override void Initialize()
    {
        Commands.ChatCommands.Add(new Command("your.permission", YourCommand, "yourcmd"));
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            Commands.ChatCommands.RemoveAll(c => c.CommandDelegate.Method?.DeclaringType?.Assembly == asm);
        }
        base.Dispose(disposing);
    }

    private void YourCommand(CommandArgs args)
    {
        args.Player.SendSuccessMessage("Hello from YourPlugin!");
    }
}
```

## Coding Standards

- **Language**: C# 12 / .NET 9
- **Style**: Follow `.editorconfig`
- **Nullable**: Enable nullable reference types
- **ImplicitUsings**: Enabled - don't add unnecessary usings
- **Naming**: PascalCase for public, _camelCase for private fields
- **Permissions**: Always prefix with `luminire.<plugin>.<action>`
- **Logging**: Use `Luminire.Core.LuminireLog` for console logging
- **Config**: Use `ConfigBase<T>` for JSON configs
- **Safety**: Check `RealPlayer`, null checks for `TSPlayer`

### Analyzer Rules

- Avoid sync-over-async
- Use `SendErrorMessage` / `SendSuccessMessage` appropriately
- Don't block main thread
- Handle exceptions in commands

## Commit Messages

We follow Conventional Commits:

```
feat: add new economy plugin
fix: resolve null reference in Essentials back command
docs: update installation guide
chore: bump TShock to 6.1.0
refactor: move config logic to Core
```

Types: `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `chore`, `ci`

## Pull Request Process

1. Update README.md if needed
2. Update CHANGELOG.md (if exists) or describe changes in PR
3. Ensure build passes: `dotnet build -c Release` with no warnings
4. Test on local TShock 6.1 server
5. PR title should follow conventional commits
6. Link related issues
7. Request review from maintainers

### PR Checklist

- [ ] Code builds without warnings
- [ ] Tested on TShock 6.1.0 / 1.4.5.6
- [ ] No binary DLLs committed
- [ ] Follows coding standards
- [ ] Permissions documented
- [ ] Config has defaults and reload support
- [ ] README updated if adding new plugin

## Reporting Bugs

Use Bug Report template. Include:

- TShock version
- Plugin version
- .NET version
- Steps to reproduce
- Expected vs actual behavior
- Server logs / stacktrace

## Feature Requests

Use Feature Request template. Describe:

- Problem / use case
- Proposed solution
- Alternatives considered
- Would you like to implement it?

## Questions?

Open a Discussion or Issue!

Thank you for contributing! ❤️
