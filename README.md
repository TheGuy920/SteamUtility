# SteamUtility
## Generalized Steam Utility for any game that combines registry utility, SteamAPI.dll, and [Steamworks.NET](https://steamworks.github.io/)

[![NuGet version (SteamUtility)](https://img.shields.io/nuget/v/SteamUtility.svg)](https://www.nuget.org/packages/SteamUtility/)  [![.NET 6.0](https://github.com/TheGuy920/smSteamUtility/actions/workflows/dotnet-desktop.yml/badge.svg?branch=main)](https://github.com/TheGuy920/smSteamUtility/actions/workflows/dotnet-desktop.yml)


```csharp
Steam steam = new("Scrap Mechanic", 387990);

// Connect to game info
if (steam.ConnectToGame())
    Debug.WriteLine("Sucessfully linked to Scrap Mechanic");

// Connect to steam
if (steam.ConnectToSteam())
    Debug.WriteLine("Sucessfully conected to Steam");

// load Avatar Bitmaps
steam.LoadAvatars();

// Avatar Bitmaps
Bitmap img_snall = SmallAvatar;
Bitmap img_med = MediumAvatar;
Bitmap img_large = LargeAvatar;

// base class
DirectoryInfo steam_dir = steam.SteamDirectory;
string display_name = steam.DisplayName;
string user_name = steam.UserName;
CSteamID steam_id = steam.SteamId;
string language = steam.SteamLanguage; 
DirectoryInfo game_dir = steam.GameDirectory; 
bool game_installed = steam.GameInstalled;
bool game_running = steam.GameRunning;
bool game_updating = steam.GameUpdating;
```

### Running `RefreshGameStats()` will update the values [`GameInstalled`, `GameRunning`, `GameUpdating`]

### Also gives full access to the [Offical Steamworks API](https://partner.steamgames.com/doc/sdk/api) via the [C# Steamworks API](https://steamworks.github.io/)


