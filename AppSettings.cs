namespace dotnet_reflection
{
    internal class AppSettings : ConfigurationComponentBase
    {
        [ConfigurationItemAttribute("locale", ProviderType.ConfigurationManager)]
        public string? Locale
        {
            get => ReadSetting(nameof(Locale));
            set => WriteSetting(nameof(Locale), value);
        }

        [ConfigurationItemAttribute("maxRequestPerSecond", ProviderType.File)]
        public int? MaxRequestPerSecond
        {
            get => ReadIntSetting(nameof(MaxRequestPerSecond));
            set => WriteSetting(nameof(MaxRequestPerSecond), value);
        }

        [ConfigurationItemAttribute("memoryLimitInGb", ProviderType.File)]
        public float? MemoryLimitInGb
        {
            get => ReadFloatSetting(nameof(MemoryLimitInGb));
            set => WriteSetting(nameof(MemoryLimitInGb), value);
        }

        [ConfigurationItemAttribute("retryTimeout", ProviderType.File)]
        public TimeSpan? RetryTimeout
        {
            get => ReadTimeSpanSetting(nameof(RetryTimeout));
            set => WriteSetting(nameof(RetryTimeout), value);
        }
    }
}
