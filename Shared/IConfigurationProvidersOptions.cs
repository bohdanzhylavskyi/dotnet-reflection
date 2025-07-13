using System.Configuration;

namespace Shared
{
    public class FileConfigurationProviderOptions
    {
        public required string FilePath;
    }

    public class ConfigurationManagerConfigurationProviderOptions
    {
        public required ConfigurationUserLevel ConfigurationUserLevel;
    }

    public interface IConfigurationProvidersOptions
    {
        public FileConfigurationProviderOptions ForFileConfigurationProvider();
        public ConfigurationManagerConfigurationProviderOptions ForConfigurationManagerConfigurationProvider();
    }
}
