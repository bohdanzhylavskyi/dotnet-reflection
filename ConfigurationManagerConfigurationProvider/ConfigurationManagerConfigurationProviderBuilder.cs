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
}
