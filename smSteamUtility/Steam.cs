using smSteamUtility.Extensions;
using smSteamUtility.Util;
using Steamworks;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace smSteamUtility
{
    public class Steam
    {
        private bool SteamInitialized { get; set; }
        private bool GameInitialized { get; set; }
        public DirectoryInfo SteamDirectory { get; internal set; }
        public string DisplayName { get; internal set; }
        public string UserName { get; internal set; }
        public CSteamID SteamId { get; internal set; }
        public string SteamLanguage { get; internal set; }
        public DirectoryInfo GameDirectory { get; internal set; }
        public bool GameInstalled { get; internal set; }
        public bool GameRunning { get; internal set; }
        public bool GameUpdating { get; internal set; }

        #region LoadSteam
        public bool ConnectToSteam()
        {
            this.SteamDirectory = new(Utility.GetRegVal<string>("Software\\Valve\\Steam", "SteamPath").ToValidPath());
            
            if(this.SteamInitialized != true)
                if (!SteamAPI.Init())
                    return false;
                else
                    this.SteamInitialized = true;

            this.UserName = SteamFriends.GetPersonaName();
            this.SteamId = SteamUser.GetSteamID();
            
            return true;
        }
        #endregion

        #region LocateGameInstallation
        public bool ConnectToGame()
        {
            this.RefreshGameStats();
            this.DisplayName = Utility.GetRegVal<string>("Software\\Valve\\Steam", "LastGameNameUsed");
            this.SteamLanguage = Utility.GetRegVal<string>("Software\\Valve\\Steam", "Language").CapitilzeFirst();

            if (!Directory.Exists(Path.Combine(this.SteamDirectory.FullName, "steamapps", "common", "Scrap Mechanic")))
            {
                string[] fileContents = File.ReadAllText(Path.Combine(this.SteamDirectory.FullName, "steamapps", "libraryfolders.vdf")).Split("\"");
                foreach (string path in fileContents)
                {
                    string smPath = Path.Combine(path, "steamapps", "common", "Scrap Mechanic");
                    if (Directory.Exists(smPath))
                    {
                        this.GameDirectory = new(smPath.Replace("\\\\", "\\"));
                        break;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RefreshGameStats()
        {
            this.GameInstalled = Utility.GetRegVal<bool>("Software\\Valve\\Steam\\Apps\\387990", "Installed");
            this.GameUpdating = Utility.GetRegVal<bool>("Software\\Valve\\Steam\\Apps\\387990", "Updating");
            this.GameRunning = Utility.GetRegVal<bool>("Software\\Valve\\Steam\\Apps\\387990", "Running");
        }
        #endregion

        public Bitmap SmallAvatar { get; internal set; }
        public Bitmap MediumAvatar { get; internal set; }
        public Bitmap LargeAvatar { get; internal set; }

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