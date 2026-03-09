# Legacy Console Launcher

![Windows](https://img.shields.io/badge/Windows-7%2B-0078D6?style=flat-square&logo=windows&logoColor=white)
![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=flat-square&logo=csharp&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

A lightweight launcher for the **Minecraft Legacy Console PC Port**.

It provides a simple way to launch the game, manage accounts, track playtime, and install/update the game without using the command line.

<p align="center">
<img width="533" height="334" alt="Launcher Screenshot" src="https://github.com/user-attachments/assets/f2a679c8-6c99-4614-b548-f70c01a1e5b7" />
</p>

---

# Table of Contents

- [Download](#download)
- [Why this launcher?](#why-this-launcher)
- [Features](#features)
- [Installation](#installation)
- [How to Use](#how-to-use)
- [Automatic Updates](#automatic-updates)
- [Data Storage](#data-storage)
- [Requirements](#requirements)
- [For Developers](#for-developers)
- [Troubleshooting](#troubleshooting)
- [License](#license)

---

# Download

Download the latest version from the **Releases** page:

https://github.com/OxyZin/LegacyConsoleLauncher/releases

---

# Why this launcher?

Many launchers for the Legacy Console PC Port are built with heavy frameworks.  
This project focuses on **simplicity, speed, and compatibility**.

### Lightweight

Built with **WinForms and C#**, the launcher starts instantly and uses very little memory compared to Electron-based launchers.

### Compatible

Runs on **Windows 7 and newer**, making it usable on older systems and lightweight setups.

### Simple and practical

No unnecessary features or complicated setup.

Just open the launcher, select your account, and play.

### Built for the community

Designed specifically for the **Minecraft Legacy Console PC Port**, with features useful for both players and developers.

---

# Features

- Multiple account support
- Playtime tracking per account
- Launch the game with a custom username
- Fullscreen toggle
- Automatic game detection
- Manual game folder selection
- Drag & drop support for:
  - `Minecraft.Client.exe`
  - the folder containing it
- Automatically remembers saved accounts
- Opens the game folder directly from the launcher
- Automatic settings saving
- Built-in installer and updater
- Lightweight **WinForms** application

---

# Installation

1. Download the latest release.
2. Extract the ZIP file.
3. Run:

```
LegacyConsoleLauncher.exe
```

No additional setup is required.

---

# How to Use

1. Run `LegacyConsoleLauncher.exe`
2. Select or enter a **username**
3. (Optional) enable **Fullscreen**
4. Click **Set Game Folder**
5. Select the folder containing:

```
Minecraft.Client.exe
```

6. Click **Launch Game**

You can also **drag & drop** either the game folder or `Minecraft.Client.exe` directly onto the launcher window.

---

# Automatic Updates

The launcher includes a **built-in installer and update system** for the nightly builds of the Minecraft Legacy Console PC Port.

### What it does

- Checks for updates automatically on startup
- Detects if the installed build is outdated
- Allows manual updates via the UI
- Downloads new builds directly from the repository
- Shows a progress dialog during downloads

### Update system features

- Commit hash detection from nightly builds
- Automatic installation for first-time users
- Executable replacement for updates
- Temporary file cleanup after installation
- Progress reporting with percentage updates

### Internal helper methods

The update system is implemented in `Form1.Updates.cs`.

Main methods include:

```
GetNightlyCommitAsync()
GetInstalledCommit()
SaveInstalledCommit()
DownloadStringWithUserAgentAsync()
DownloadFileWithProgressAsync()
InstallGameAsync()
UpdateGameExeAsync()
CheckForUpdatesOnStartupAsync()
```

### UI Integration

The update system integrates with the launcher UI:

- `ShowProgressForm()` / `CloseProgressForm()`  
  Controls the progress dialog (`Form2`)

- `checkforLink_LinkClicked`  
  Manual update trigger

---

# Data Storage

The launcher stores configuration and account data locally.

Generated files:

```
accounts.txt
gamepath.txt
releaseInfo.txt
```

### accounts.txt

Stores usernames and playtime in seconds.

Format:

```
username|playtime_in_seconds
```

Example:

```
Steve|7200
Alex|3500
```

### gamepath.txt

Stores the location of:

```
Minecraft.Client.exe
```

### releaseInfo.txt

Stores the installed nightly commit hash used by the update system.

Example:

```
commit=8f3d2c1
```

---

# Requirements

- Windows 7 or newer
- .NET Framework 4.7.2
- Minecraft Legacy Console PC Port

---

# For Developers

The project is built using:

- **C#**
- **WinForms**
- **.NET Framework 4.7.2**
- **C# 7.3**

### Build Instructions

1. Open the solution in **Visual Studio**
2. Ensure the target framework is:

```
.NET Framework 4.7.2
```

3. Build the project.

### Expected fields in `Form1`

The update system expects the following fields declared in other partial classes:

```
nightlyReleaseUrl
nightlyZipUrl
nightlyExeUrl
releaseInfoFile
gameInstallDir
exePath
progressForm
checkforLink
```

---

# Troubleshooting

### Launcher cannot find the game

Use **Set Game Folder** and select the folder containing:

```
Minecraft.Client.exe
```

### Update fails

- Check your internet connection
- Ensure GitHub is accessible
- Try restarting the launcher

### Antivirus warnings

Some antivirus software may flag unsigned executables.  
This launcher is **open source**, and the code can be reviewed.

---

# License

MIT License

© 2026 **GatoWare**
