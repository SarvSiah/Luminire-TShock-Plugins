# Luminire.Template

Starter template for creating your own Luminire TShock 6.1 plugin.

## How to Use

1. Copy this folder to `src/Luminire.YourPlugin`
2. Rename `.csproj` file
3. Edit `Plugin.cs`:
   - Namespace
   - Class name
   - Name, Author, Description, Version
4. Edit `Configuration.cs` if needed
5. Add to solution:
   ```bash
   dotnet sln add src/Luminire.YourPlugin/Luminire.YourPlugin.csproj
   ```
6. Build:
   ```bash
   dotnet build -c Release
   ```

## What It Includes

- ConfigBase usage
- Command registration
- Hook registration (GameInitialize, NetGreetPlayer, Reload, PostLogin)
- Proper Dispose pattern
- Logging via Luminire.Core
- Example commands: `/template`, `/tadmin`

## Permissions

- `luminire.template.use`
- `luminire.template.admin`

## Compatibility

- TShock 6.1.0 / Terraria 1.4.5.6 / .NET 9.0 / OTAPI 3.3.11 / ApiVersion 2.1

## Next Steps

- Add your own commands
- Add database logic
- Add more hooks
- Remove example code you don't need

See `docs/DEVELOPMENT.md` for detailed guide.
