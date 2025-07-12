namespace dotnet_reflection
{
    internal class AppSettings : ConfigurationComponentBase
    {
        [ConfigurationItemAttribute("theme", ProviderType.File)]
        public string Theme
        {
            get => ReadSetting<string>(nameof(Theme));
            set => WriteSetting(nameof(Theme), value);
        }

        [ConfigurationItemAttribute("limit", ProviderType.File)]
        public int Limit
        {
            get => ReadSetting<int>(nameof(Limit));
            set => WriteSetting(nameof(Limit), value);
        }
    }
}
