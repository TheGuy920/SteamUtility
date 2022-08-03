namespace smSteamUtility.Extensions
{
    internal static class Extensions
    {
        public static string CapitilzeFirst(this string s)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s);
        }
        public static string ToValidPath(this string s)
        {
            return System.IO.Path.GetFullPath(CapitilzeFirst(s).Replace("/", "\\"));
        }
    }
}
