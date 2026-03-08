# Legacy Console Launcher

![Windows](https://img.shields.io/badge/Windows-7%2B-0078D6?style=flat-square&logo=windows&logoColor=white)
![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=flat-square&logo=csharp&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

A lightweight launcher for the **Minecraft Legacy Console PC Port**.

It provides a simple way to launch the game, manage accounts and track playtime without needing to use the command line.

<p align="center">
<img width="366" height="334" alt="image" src="https://github.com/user-attachments/assets/97cd7261-6e35-43c1-9d97-a4ba2a0fc026" />
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

MIT License  
© 2026 GatoWare
