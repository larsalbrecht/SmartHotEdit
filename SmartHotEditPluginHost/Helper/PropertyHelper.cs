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
            /*
            foreach (SettingsProperty sp in properties.Settings.Default.Properties)
            {
                if (sp.Name == key)
                {
                    return true;
                }
            }*/
            return false;
        }

        public static void CreateProperty(String propertyKey, object defaultValue, Type propertyType)
        {
            /*
            SettingsProperty property = new SettingsProperty(propertyKey);
            property.DefaultValue = defaultValue;
            property.PropertyType = propertyType;
            property.Provider = properties.Settings.Default.Providers["LocalFileSettingsProvider"];
            property.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());
            properties.Settings.Default.Properties.Add(property);
            properties.Settings.Default.Reload();*/
        }

    }
}
