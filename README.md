# smSteamUtility
## Steam Utility for Scrap Mechanic that combines registry utility, SteamAPI.dll, and [Steamworks.NET](https://steamworks.github.io/)

[![NuGet version (smSteamUtility)](https://img.shields.io/nuget/v/smSteamUtility.svg)](https://www.nuget.org/packages/smSteamUtility/)[![Build & Test]


```csharp
Steam steam = new();

// Connect to steam
if (steam.ConnectToSteam())
    Debug.WriteLine("Sucessfully conected to Steam");

// Connect to game
if (steam.ConnectToGame())
    Debug.WriteLine("Sucessfully linked to Scrap Mechanic");

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

