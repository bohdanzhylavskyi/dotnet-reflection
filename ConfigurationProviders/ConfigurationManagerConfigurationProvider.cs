using System.Configuration;

namespace dotnet_reflection.ConfigurationProviders
{
    internal class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        public string? Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        public void Save()
        {
        }

        public void Set(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            
            configFile.Save(ConfigurationSaveMode.Modified);
            
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
