using Shared;
using System.Configuration;

namespace ConfigurationManagerConfigurationProvider
{
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        private readonly Configuration configuration;

        public ConfigurationManagerConfigurationProvider(ConfigurationManagerConfigurationProviderOptions options)
        {
            configuration = ConfigurationManager.OpenExeConfiguration(options.ConfigurationUserLevel);
        }

        public ProviderType GetProviderType()
        {
            return ProviderType.ConfigurationManager;
        }

        public string? Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        public void Save()
        {
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }

        public void Set(string key, string value)
        {
            var settings = configuration.AppSettings.Settings;

            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
        }
    }
}
