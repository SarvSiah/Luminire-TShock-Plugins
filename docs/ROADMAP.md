# Roadmap - Luminire TShock Plugins

## Vision

To become the most reliable, well-documented, and community-friendly TShock plugin suite for TShock 6.1+.

## Version 1.0 (Current) - Foundation

- [x] Repository scaffold for TShock 6.1.0 / .NET 9.0 / Terraria 1.4.5.6
- [x] Luminire.Core shared library
- [x] Luminire.Essentials (clear, back, help, info, join messages)
- [x] Luminire.Template starter
- [x] CI/CD with GitHub Actions
- [x] Documentation (README, CONTRIBUTING, INSTALLATION, DEVELOPMENT, COMPATIBILITY)
- [x] MIT License, CoC, Security Policy

## Version 1.1 - Essentials Expansion

- [ ] Home system (`/sethome`, `/home`, `/delhome`, `/homes`)
- [ ] Warp system improvements
- [ ] TPA system (`/tpa`, `/tpaccept`, `/tpdeny`)
- [ ] Better `/back` with multiple positions
- [ ] AFK detection and `/afk`
- [ ] Playtime tracking
- [ ] First join handling

## Version 1.2 - Chat & Economy

- [ ] **Luminire.Chat**
  - Chat formatting with prefixes/suffixes
  - Chat channels (global, local, staff, trade)
  - Anti-spam, caps filter
  - Discord webhook integration
  - @mentions

- [ ] **Luminire.Economy**
  - Currency system
  - `/pay`, `/balance`, `/baltop`
  - Shop with NPC or command
  - Rewards for playtime, kills, fishing
  - SQLite/MySQL support

## Version 1.3 - Administration

- [ ] **Luminire.Admin**
  - Vanish `/vanish`
  - Staff chat `/sc`
  - Spy on commands/PMs
  - Inventory inspection `/invsee`
  - Better broadcast `/bc`, `/bcraw`
  - Freeze players

- [ ] **Luminire.Moderation**
  - Advanced ban/mute with durations
  - Ban sync across servers
  - Warning system
  - Auto-moderation

## Version 1.4 - World & Protection

- [ ] **Luminire.Protection**
  - Region enhancements
  - Anti-grief logging
  - Rollback tool

- [ ] **Luminire.World**
  - World edit commands (limited)
  - Auto-save control
  - Blood moon / event control

## Version 2.0 - Advanced Features

- [ ] **Luminire.Ranks**
  - Playtime-based ranks
  - Automatic group promotion
  - Rankup commands

- [ ] **Luminire.Events**
  - Automated events (boss rush, fishing contest)
  - Scheduled commands
  - Event rewards

- [ ] **Luminire.Discord**
  - Full Discord bot integration
  - Server status, chat relay
  - Whitelist via Discord roles

- [ ] **Luminire.API**
  - REST API extensions
  - Web dashboard (optional)

## Long Term

- [ ] Plugin marketplace / registry
- [ ] Localization (multi-language)
- [ ] Database abstraction layer
- [ ] Performance monitoring
- [ ] Support for TShock 6.2+ / .NET 10 when released

## Community Requests

Have an idea? Open a Feature Request!

- Use template: `.github/ISSUE_TEMPLATE/feature_request.yml`
- Or Discussions: https://github.com/SarvSiah/Luminire-TShock-Plugins/discussions

## How to Contribute to Roadmap

- Pick an item from roadmap
- Open issue saying you want to work on it
- Fork, implement, PR

We prioritize:

1. Stability and bug fixes
2. Features requested by community
3. Features that help server owners
4. Cool experimental features

## Release Cadence

- **Patch** (1.0.x): Bug fixes, weekly as needed
- **Minor** (1.x.0): New features, monthly
- **Major** (x.0.0): Breaking changes, TShock major updates

---

Want to influence roadmap? Star the repo, open issues, join discussions!
