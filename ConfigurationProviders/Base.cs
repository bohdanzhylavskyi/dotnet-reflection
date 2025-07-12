namespace dotnet_reflection.ConfigurationProviders
{
    public interface IConfigurationProvider
    {
        // TODO: Add Generic
        public string? Get(string key);
        public void Set(string key, string value);
        public void Save();
    }
}
