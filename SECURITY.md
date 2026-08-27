# Security Policy

## Supported Versions

We actively support the following versions with security updates:

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

TShock 6.1.0 / Terraria 1.4.5.6 / .NET 9.0 is the only supported target. Older TShock versions are not supported.

## Reporting a Vulnerability

We take security seriously. If you discover a security vulnerability within Luminire TShock Plugins, please follow these steps:

### 1. Do NOT open a public issue

Please do not report security vulnerabilities through public GitHub issues.

### 2. Email us privately

Send details to: **security@luminire.example** (or contact @SarvSiah via GitHub private vulnerability reporting)

If you don't get a response within 48 hours, please open a GitHub issue with title `[SECURITY] Please check private report` without details.

### 3. Include details

Please include:

- Type of vulnerability (e.g., RCE, privilege escalation, data leak)
- Affected plugin and version
- Steps to reproduce
- Potential impact
- Any suggested fix (optional)

### 4. What to expect

- Acknowledgment within 48 hours
- Initial assessment within 1 week
- Fix and release timeline depending on severity
- Credit in release notes if desired (or anonymous)

## Severity Levels

- **Critical**: RCE, auth bypass, data loss
- **High**: Privilege escalation, major data leak
- **Medium**: DoS, minor permission bypass
- **Low**: Info disclosure, non-exploitable edge cases

## Best Practices for Server Owners

- Always keep TShock and Luminire plugins updated to latest release
- Don't give `luminire.admin` to untrusted groups
- Backup `tshock.sqlite` and `tshock/` folder regularly
- Use strong passwords for accounts
- Review permissions after installing new plugins

## Disclosure Policy

- We will coordinate fix and release
- We will publish security advisory after fix is released
- We request 90 days embargo before public disclosure (or sooner if mutually agreed)

Thank you for helping keep Luminire and the Terraria community safe!
