using System;
using System.Configuration;
using System.Linq;

namespace SmartHotEditPluginHost.Helper
{
    public static class PropertyHelper
    {
        public static bool PropertiesHasKey(string key)
        {
            return Properties.Settings.Default.Properties.Cast<SettingsProperty>().Any(sp => sp.Name == key);
        }

        public static void CreateProperty(string propertyKey, object defaultValue, Type propertyType)
        {
            if (PropertiesHasKey(propertyKey)) return;

            var property = new SettingsProperty(propertyKey)
            {
                DefaultValue = defaultValue,
                PropertyType = propertyType,
                Provider = Properties.Settings.Default.Providers["LocalFileSettingsProvider"],
                IsReadOnly = false,
            };
            property.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());
            Properties.Settings.Default.Properties.Add(property);
            
            //Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}