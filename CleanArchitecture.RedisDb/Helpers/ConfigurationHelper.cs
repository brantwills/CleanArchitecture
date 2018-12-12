using System;
using System.Configuration;

namespace CleanArchitecture.RedisDb.Helpers
{
    public static class ConfigurationHelper
    {
        internal static T Get<T>(string appSettingsKey, T defaultValue)
        {
            // string text = ConfigurationManager.AppSettings[appSettingsKey];
            string text = "";
            if (string.IsNullOrWhiteSpace(text))
                return defaultValue;
            try
            {
                var value = Convert.ChangeType(text, typeof(T));
                return (T)value;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
