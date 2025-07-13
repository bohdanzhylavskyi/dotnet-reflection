namespace Shared
{
    public interface IConfigurationProvider
    {
        public ProviderType GetProviderType();

        public string? Get(string key);
        public void Set(string key, string value);
        public void Save();
    }
}
