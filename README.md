# Legacy Console Launcher

![Windows](https://img.shields.io/badge/Windows-7%2B-0078D6?style=for-the-badge&logo=windows)
![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=csharp)
![Downloads](https://img.shields.io/github/downloads/oxyzin/LegacyConsoleLauncher/total?style=for-the-badge)
![License](https://img.shields.io/github/license/oxyzin/LegacyConsoleLauncher?style=for-the-badge)

A lightweight launcher for the **Minecraft Legacy Console PC Port**.

It provides a simple way to launch the game, manage accounts and track playtime without needing to use the command line.

<p align="center">
<img width="846" height="506" alt="image" src="https://github.com/user-attachments/assets/e23b12be-59af-4f3d-b822-abe0ba1d6068" />
</p>

---

## Download

Download the latest version from the **Releases** page:

https://github.com/OxyZin/LegacyConsoleLauncher/releases

---
## Why this launcher?

Many launchers for the Legacy Console PC Port are built with heavy frameworks.  
This project focuses on **simplicity, speed and compatibility**.

### Lightweight
Built with **WinForms and C#**, the launcher starts instantly and uses very little memory compared to Electron-based launchers.

### Compatible
Runs on **Windows 7 and newer**, making it usable on older systems and lightweight setups.

### Simple and practical
No unnecessary features or complicated setup.  
Just open the launcher, select your account and play.

### Built for the community
Designed specifically for the **Minecraft Legacy Console PC Port**, with features that are useful for players and developers:

- Account management
- Playtime tracking
- Automatic game detection
- Simple configuration

## Features

- Multiple account support
- Playtime tracking per account
- Launch the game with a custom username
- Fullscreen toggle
- Automatically remembers saved accounts
- Auto-detects the game executable
- Manual game folder selection
- Drag & drop support for:
  - `Minecraft.Client.exe`
  - the folder containing it
- Opens the game folder directly from the launcher
- Saves launcher settings automatically
- Lightweight WinForms application

---

## How to Use

1. Run `LegacyConsoleLauncher.exe`
2. Select or enter a **username**
3. (Optional) enable **Fullscreen**
4. Click **Set Game Folder** and select the folder containing:


Minecraft.Client.exe


5. Click **Launch Game**

You can also **drag and drop** either the game folder or `Minecraft.Client.exe` directly onto the launcher.

---

## Data Storage

The launcher automatically creates the following files:


accounts.txt
gamepath.txt


### accounts.txt
Stores usernames and their playtime:


username|playtime_in_seconds


Example:


Steve|7200
Alex|3500


### gamepath.txt
Stores the location of `Minecraft.Client.exe`.

---

## Requirements

- Windows 7 or newer
- Minecraft Legacy Console PC Port

---

## License

## Updates

This file summarizes the update-related changes added to the project (see `Form1.Updates.cs`).

What was added

- Automatic nightly-check helpers
  - `GetNightlyCommitAsync()` downloads the release page and extracts the commit hash.
  - `GetInstalledCommit()` / `SaveInstalledCommit()` read/write the installed commit from the release info file.

- Download helpers with custom User-Agent and progress
  - `DownloadStringWithUserAgentAsync(string url)` uses `WebClient` and sets a `User-Agent` header.
  - `DownloadFileWithProgressAsync(string url, string outputPath, string statusText)` shows a progress UI while downloading and reports percentage updates.

- Install and update flows
  - `InstallGameAsync()` downloads the nightly ZIP, extracts it to a temporary folder, moves it into the install folder, detects the game executable and saves the installed commit.
  - `UpdateGameExeAsync()` downloads the updated game executable, replaces the existing exe and saves the installed commit.

- UI integration
  - `ShowProgressForm()` / `CloseProgressForm()` manage a `Form2` progress dialog (`progressForm`).
  - `CheckForUpdatesOnStartupAsync()` checks for the latest nightly commit on startup and prompts the user to install/update (also toggles `checkforLink` visibility).
  - `checkforLink_LinkClicked` is a manual trigger bound to a `LinkLabel` that installs/updates when clicked.

Error handling and cleanup

- Both install and update flows perform cleanup of temporary files/directories and close the progress UI on exceptions. Exceptions are re-thrown to let calling UI show user-facing messages.

Notes for maintainers

- The update code expects several fields to be present elsewhere in `Form1` (declared in other partial files):
  - `nightlyReleaseUrl`, `nightlyZipUrl`, `nightlyExeUrl`
  - `releaseInfoFile`, `gameInstallDir`, `exePath`
  - `progressForm` (type `Form2`) and `checkforLink` (type `LinkLabel`)

- The project targets .NET Framework 4.7.2 and uses C# 7.3 language features.

How to test

1. Build the project in Visual Studio (target .NET Framework 4.7.2).
2. Run the launcher and confirm the startup update check logic runs (or trigger the `checkforLink` link).
3. Observe the progress dialog while downloads happen and verify that the game installs/updates and `releaseInfoFile` is written.

MIT License  
© 2026 GatoWare

## Star History

<a href="https://www.star-history.com/?repos=OxyZin%2FLegacyConsoleLauncher&type=date&legend=top-left">
 <picture>
   <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/image?repos=OxyZin/LegacyConsoleLauncher&type=date&theme=dark&legend=top-left" />
   <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/image?repos=OxyZin/LegacyConsoleLauncher&type=date&legend=top-left" />
   <img alt="Star History Chart" src="https://api.star-history.com/image?repos=OxyZin/LegacyConsoleLauncher&type=date&legend=top-left" />
 </picture>
</a>
