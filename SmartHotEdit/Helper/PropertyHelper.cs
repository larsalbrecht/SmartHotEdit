using SmartHotEditPluginHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SmartHotEdit.Helper
{
    class PropertyHelper
    {
        public static bool PropertiesHasKey(string key)
        {
            foreach (SettingsProperty sp in Properties.Settings.Default.Properties)
            {
                if (sp.Name == key)
                {
                    return true;
                }
            }
            return false;
        }

        public static void CreateProperty(String propertyKey, object defaultValue, Type propertyType)
        {
            SettingsProperty property = new SettingsProperty(propertyKey);
            property.DefaultValue = defaultValue;
            property.PropertyType = propertyType;
            property.Provider = Properties.Settings.Default.Providers["LocalFileSettingsProvider"];
            property.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());
            Properties.Settings.Default.Properties.Add(property);
            Properties.Settings.Default.Reload();
        }

    }
}
