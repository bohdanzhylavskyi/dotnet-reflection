using Shared;

namespace dotnet_reflection.Exceptions
{
    public class ConfigurationProviderBuilderIsNotDefined : BaseException
    {
        public ConfigurationProviderBuilderIsNotDefined(string pluginPath):
            base($"Configuration Provider Builder is not defined in plugin '{pluginPath}'.")
        {
        }
    }
}
