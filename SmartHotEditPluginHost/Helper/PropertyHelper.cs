using System;
using System.Configuration;
using System.Linq;
using SmartHotEditPluginHost.Properties;

namespace SmartHotEditPluginHost.Helper
{
    public static class PropertyHelper
    {
        private static bool PropertiesHasKey(string key)
        {
            return Settings.Default.Properties.Cast<SettingsProperty>().Any(sp => sp.Name == key);
        }

        public static void CreateProperty(string propertyKey, object defaultValue, Type propertyType)
        {
            if (PropertiesHasKey(propertyKey)) return;

            var property = new SettingsProperty(propertyKey)
            {
                DefaultValue = defaultValue,
                PropertyType = propertyType,
                Provider = Settings.Default.Providers["LocalFileSettingsProvider"],
                IsReadOnly = false
            };
            property.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());
            Settings.Default.Properties.Add(property);

            //Properties.Settings.Default.Save();
            Settings.Default.Reload();
        }
    }
}