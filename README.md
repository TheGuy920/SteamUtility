# smSteamUtility
## Steam Utility for Scrap Mechanic that combines SteamAPI.dll, registry utility, [Steamworks.NET](https://steamworks.github.io/)


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

### Running `RefreshGameStats()` will update the values [`is_running`, `is_installed`, `is_updating`]

### Also gives full access to the [Offical Steamworks API](https://partner.steamgames.com/doc/sdk/api) via the [C# Steamworks API](https://steamworks.github.io/)

