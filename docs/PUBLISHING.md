# Publishing Guide

This guide explains how to publish a new release of Luminire TShock Plugins.

## Prerequisites

- Write access to `SarvSiah/Luminire-TShock-Plugins`
- All PRs merged to `main`
- CI build passing
- CHANGELOG.md updated

## Steps

### 1. Update Version

Update version in:

- `Directory.Build.props` -> `<VersionPrefix>1.0.0</VersionPrefix>`
- `src/Luminire.Core/Luminire.Core.csproj` -> `<Version>1.0.0</Version>`
- `src/Luminire.Essentials/Luminire.Essentials.csproj` -> `<Version>1.0.0</Version>`
- etc.

Or rely on GitHub Actions to pass version via `-p:Version=X.X.X` during release build (recommended).

### 2. Update CHANGELOG.md

Move items from `[Unreleased]` to new version section:

```markdown
## [1.0.0] - 2026-08-27

### Added
- ...
```

### 3. Commit and Push

```bash
git add .
git commit -m "chore: bump version to v1.0.0"
git push origin main
```

### 4. Create Tag

```bash
git tag v1.0.0
git push origin v1.0.0
```

This triggers `.github/workflows/release.yml` which:

- Builds Release DLLs with version from tag
- Creates zip `Luminire-TShock-Plugins-v1.0.0-net9.0-TShock6.1.0.zip`
- Creates GitHub Release with changelog and assets

### 5. Verify Release

- Go to https://github.com/SarvSiah/Luminire-TShock-Plugins/releases
- Check release notes and assets
- Download zip and test on local TShock 6.1.0 server
- Verify DLLs load

### 6. Announce

- Discord
- TShock forums
- Terraria community

## Manual Release (without tag)

You can manually trigger release workflow:

1. Go to Actions -> Release -> Run workflow
2. Enter version `v1.0.0`
3. Run

This will create release without pushing tag first, but still creates tag? Actually manual dispatch uses input version but does not push tag - you should still create tag.

## Hotfix Release

For hotfix `v1.0.1`:

```bash
git checkout main
git pull
# fix bug
git commit -m "fix: critical bug in Essentials"
git push
git tag v1.0.1
git push origin v1.0.1
```

## Pre-releases

For beta/alpha:

```bash
git tag v1.1.0-beta.1
git push origin v1.1.0-beta.1
```

Release workflow will mark as prerelease if tag contains `-pre`, `-beta`, `-alpha`.

## Versioning

We use Semantic Versioning:

- **MAJOR**: Breaking changes, TShock major update
- **MINOR**: New features, non-breaking
- **PATCH**: Bug fixes

Examples:

- `v1.0.0` -> First stable
- `v1.1.0` -> New plugin added
- `v1.0.1` -> Bug fix
- `v2.0.0` -> TShock 6.2 or breaking API

## Checklist Before Release

- [ ] All plugins build in Release mode without warnings
- [ ] Tested on TShock 6.1.0 / 1.4.5.6 server
- [ ] CHANGELOG.md updated
- [ ] README.md compatibility table updated if needed
- [ ] No binary DLLs committed
- [ ] CI passing
- [ ] Permissions documented

## Rollback

If release has critical bug:

1. Delete release and tag on GitHub (if needed)
2. Fix bug on main
3. Create new patch version tag

Do not force-push to main.

## Publishing to NuGet (Optional)

If you want to publish Luminire.Core as NuGet package:

```bash
dotnet pack -c Release
dotnet nuget push src/Luminire.Core/bin/Release/Luminire.Core.1.0.0.nupkg --api-key YOUR_KEY --source https://api.nuget.org/v3/index.json
```

Not currently automated.

## Questions?

Open Discussion or contact @SarvSiah.
