# smSteamUtility
 Steam Utility for Scrap Mechanic


```csharp
Steam steam = new();
steam.ConnectToSteam();
steam.ConnectToGame();
steam.LoadAvatars();

DirectoryInfo steam_dir = steam.SteamDirectory;
string display_name = steam.DisplayName;
string user_name = steam.UserName;
CSteamID steam_id = steam.SteamId;
string language = steam.SteamLanguage; 
DirectoryInfo game_dir = steam.GameDirectory; 
bool game_installed = steam.GameInstalled;
bool game_running = steam.GameRunning;
bool game_updating = steam.GameUpdating;

Bitmap img_snall = SmallAvatar;
Bitmap img_med = MediumAvatar;
Bitmap img_large = LargeAvatar;
```

Re-Running `ConnectToSteam()` and `ConnectToGame()` will re-initialized the values if they update (some rely on the registry)
Also gives full access to the [SteamWorks API](https://steamworks.github.io/)
