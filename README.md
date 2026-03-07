# Legacy Console Launcher

![Windows](https://img.shields.io/badge/Windows-7%2B-0078D6?style=flat-square&logo=windows&logoColor=white)
![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=flat-square&logo=csharp&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

Simple launcher for the **Minecraft Legacy Console PC Port**.

This tool makes it easier to start the game with arguments, select the game folder and manage basic settings.
<p align="center">
<img width="259" height="334" src="https://github.com/user-attachments/assets/5c61a387-bae6-4c11-a42f-c33d45d8435c">
</p>

## Download

You can download the latest version from the Releases page.

https://github.com/SEUUSER/LegacyConsoleLauncher/releases

## Features

* Launch the game with a custom username
* Fullscreen toggle
* Remembers the last username
* Set game folder manually
* Drag & drop support for:

  * `Minecraft.Client.exe`
  * the folder containing it
* Automatically saves the game path
* Closes the launcher when the game starts

## How to Use

1. Run `LegacyConsoleLauncher.exe`.

2. Enter your **username**.

3. (Optional) enable **Fullscreen**.

4. Click **Set Game Folder** and select the folder containing:

   ```text
   Minecraft.Client.exe
   ```

5. Click **Launch!**

You can also **drag and drop** the game folder or `Minecraft.Client.exe` directly onto the launcher.

## Configuration

The launcher automatically creates these files:

```text
username.txt
gamepath.txt
```

They store your last used username and the game path.

## Requirements

* Windows
* Minecraft Legacy Console PC Port

## License

MIT License
© 2026 GatoWare
