using Microsoft.Win32;
using System.Reflection;

namespace smSteamUtility.Util
{
    internal static class Utility
    {
        public static T? GetRegVal<T>(string path, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path);
                if (key != null)
                    if (typeof(T) == typeof(bool))
                        return (T)(object)Convert.ToBoolean((int)key.GetValue(value));
                    else
                        return (T)key.GetValue(value);
                else
                    throw new();
            }
            catch (Exception)
            {
                return default;
            }
        }
        internal static class LoadInternalFile
        {
            public static string TextFile(string resourceName)
            {
                string ResourceFileName = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith(resourceName));
                return new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceFileName)).ReadToEnd();
            }
        }
    }
}
