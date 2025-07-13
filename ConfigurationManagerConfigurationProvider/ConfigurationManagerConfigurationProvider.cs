using Shared;
using System.Configuration;

namespace ConfigurationManagerConfigurationProvider
{
    public class ConfigurationManagerConfigurationProviderBuilder : IConfigurationProviderBuilder
    {
        public IConfigurationProvider Build(IConfigurationProvidersOptions options)
        {
            return new ConfigurationManagerConfigurationProvider(
                options.ForConfigurationManagerConfigurationProvider()
            );
        }
    }

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
        }

        public void Set(string key, string value)
        {
            //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;

            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }
    }
}
