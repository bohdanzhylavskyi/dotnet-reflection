using Shared;

namespace FileConfigurationProvider
{
    public class FileConfigurationProviderBuilder : IConfigurationProviderBuilder
    {
        public IConfigurationProvider Build(IConfigurationProvidersOptions options)
        {
            return new FileConfigurationProvider(options.ForFileConfigurationProvider());
        }
    }
}
