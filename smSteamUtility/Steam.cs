using smSteamUtility.Extensions;
using smSteamUtility.Util;
using Steamworks;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace smSteamUtility
{
    public class Steam(string GameName, uint appid)
    {
        public bool SteamInitialized { get; internal set; }
        public DirectoryInfo? SteamDirectory { get; internal set; }
        public string? DisplayName { get; internal set; }
        public string? UserName { get; internal set; }
        public CSteamID SteamId { get; internal set; }
        public string? SteamLanguage { get; internal set; }
        public DirectoryInfo? GameDirectory { get; internal set; }
        public bool GameInstalled { get; internal set; }
        public bool GameRunning { get; internal set; }
        public bool GameUpdating { get; internal set; }

        #region LoadSteam
        public bool ConnectToSteam()
        {
            this.SteamDirectory ??= new(Utility.GetRegVal<string>("Software\\Valve\\Steam", "SteamPath")!.ToValidPath());

            if (this.GameDirectory is null)
                if (!this.ConnectToGame())
                    return false;

            if (!this.SteamInitialized)
            {
                this.SteamInitialized = SteamAPI.Init();
                if (!this.SteamInitialized)
                {
                    Debug.WriteLine("SteamAPI failed to initialize.");
                    Debug.WriteLine(SteamAPI.Init());
                    Debug.WriteLine(SteamAPI.Init());
                    Debug.WriteLine(SteamAPI.Init());
                    Debug.WriteLine(SteamAPI.Init());

                    return false;
                }
            }

            this.UserName ??= SteamFriends.GetPersonaName();
            this.SteamId = SteamUser.GetSteamID();
            
            return true;
        }
        #endregion

        #region LocateGameInstallation
        public bool ConnectToGame()
        {
            this.RefreshGameStats();
            this.SteamDirectory ??= new(Utility.GetRegVal<string>("Software\\Valve\\Steam", "SteamPath")!.ToValidPath());
            this.DisplayName ??= Utility.GetRegVal<string>("Software\\Valve\\Steam", "LastGameNameUsed");
            this.SteamLanguage ??= Utility.GetRegVal<string>("Software\\Valve\\Steam", "Language")?.CapitilzeFirst();

            if (!Directory.Exists(Path.Combine(this.SteamDirectory!.FullName, "steamapps", "common", GameName)))
            {
                string[] fileContents = File.ReadAllText(
                    Path.Combine(this.SteamDirectory.FullName, "steamapps", "libraryfolders.vdf"))
                    .Split("\"");

                foreach (string path in fileContents)
                {
                    string smPath = Path.Combine(path, "steamapps", "common", GameName);
                    if (Directory.Exists(smPath))
                    {
                        this.GameDirectory = new DirectoryInfo(smPath.Replace("\\\\", "\\"));
                        break;
                    }
                }

                string steamApiDll = new DirectoryInfo(this.GameDirectory!.FullName)
                    .GetFiles("steam_api64.dll", SearchOption.AllDirectories)
                    .First().FullName;

                string destSteamApiDll = Path.Combine(Environment.CurrentDirectory, "steam_api64.dll");

                if (File.Exists(steamApiDll) && !File.Exists(destSteamApiDll))
                    File.Copy(steamApiDll, destSteamApiDll, false);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RefreshGameStats()
        {
            this.GameInstalled = Utility.GetRegVal<bool>($"Software\\Valve\\Steam\\Apps\\{appid}", "Installed");
            this.GameUpdating = Utility.GetRegVal<bool>($"Software\\Valve\\Steam\\Apps\\{appid}", "Updating");
            this.GameRunning = Utility.GetRegVal<bool>($"Software\\Valve\\Steam\\Apps\\{appid}", "Running");
        }
        #endregion

        public Bitmap? SmallAvatar { get; internal set; }
        public Bitmap? MediumAvatar { get; internal set; }
        public Bitmap? LargeAvatar { get; internal set; }

        #region LoadSteamAvatar
        public void LoadAvatars()
        {
            var http = new HttpClient();
            var result = http.GetAsync($"https://steamcommunity.com/profiles/{this.SteamId}?xml=true").GetAwaiter().GetResult();
            XElement DataObject = XElement.Parse(new StreamReader(result.Content.ReadAsStream(), Encoding.UTF8).ReadToEnd());
            System.Net.WebRequest request;
            System.Net.WebResponse response;
            foreach (XElement x in DataObject.Elements())
            {
                switch (x.Name.ToString().ToLower())
                {
                    case "avatarfull":
                        request = System.Net.WebRequest.Create(new Uri(x.Value, UriKind.Absolute));
                        response = request.GetResponse();
                        this.LargeAvatar = new(response.GetResponseStream());
                        break;
                    case "avatarmedium":
                        request = System.Net.WebRequest.Create(new Uri(x.Value, UriKind.Absolute));
                        response = request.GetResponse();
                        this.MediumAvatar = new(response.GetResponseStream());
                        break;
                    case "avataricon":
                        request = System.Net.WebRequest.Create(new Uri(x.Value, UriKind.Absolute));
                        response = request.GetResponse();
                        this.SmallAvatar = new(response.GetResponseStream());
                        break;
                }
            }
        }
        #endregion
    }
}