using smSteamUtility;

Steam steam = new("Scrap Mechanic", 387990);
File.WriteAllText("steam_appid.txt", $"{387990}");

var result = steam.ConnectToSteam();
Console.WriteLine($"result: {result}");

// Redundant since we call ConnectToSteam
// steam.ConnectToGame();

Console.WriteLine($"Steam Initialized: {steam.SteamInitialized}");
Console.WriteLine($"Steam Directory: {steam.SteamDirectory}");
Console.WriteLine($"Steam User Name: {steam.UserName}");
Console.WriteLine($"Steam ID: {steam.SteamId}");
Console.WriteLine($"Steam Language: {steam.SteamLanguage}");
Console.WriteLine($"Game Directory: {steam.GameDirectory}");
Console.WriteLine($"Game Installed: {steam.GameInstalled}");
Console.WriteLine($"Game Running: {steam.GameRunning}");
Console.WriteLine($"Game Updating: {steam.GameUpdating}");
Console.WriteLine($"Game Display Name: {steam.DisplayName}");

Console.ReadLine();
