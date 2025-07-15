using Shared;

namespace dotnet_reflection.Exceptions
{
    public class MissingConfigurationProviderException : BaseException
    {
        public MissingConfigurationProviderException(ProviderType providerType): base($"Missing configuration provider for '{providerType}' provider type.")
        {
        }
    }
}
