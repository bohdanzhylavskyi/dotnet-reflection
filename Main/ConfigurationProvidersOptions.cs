using Shared;
using System.Configuration;

namespace dotnet_reflection
{
    public class ConfigurationProvidersOptions : IConfigurationProvidersOptions
    {
        readonly static string ConfigurationFilePath = ".\\configs\\config.txt";

        public ConfigurationManagerConfigurationProviderOptions ForConfigurationManagerConfigurationProvider()
        {
            return new ConfigurationManagerConfigurationProviderOptions()
            {
                ConfigurationUserLevel = ConfigurationUserLevel.None
            };
        }

        public FileConfigurationProviderOptions ForFileConfigurationProvider()
        {
            return new FileConfigurationProviderOptions() { FilePath = ConfigurationFilePath };
        }
    }
}
