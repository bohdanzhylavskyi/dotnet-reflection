namespace Shared
{
    public interface IConfigurationProviderBuilder
    {
        public IConfigurationProvider Build(IConfigurationProvidersOptions options);
    }
}
