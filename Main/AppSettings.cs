using Shared;

namespace dotnet_reflection
{
    internal class AppSettings : ConfigurationComponentBase
    {
        [ConfigurationItem("maxRequestPerSecond", ProviderType.File)]
        public int? MaxRequestPerSecond
        {
            get => ReadIntSetting(nameof(MaxRequestPerSecond));
            set => WriteSetting(nameof(MaxRequestPerSecond), value);
        }

        [ConfigurationItem("memoryLimitInGb", ProviderType.File)]
        public float? MemoryLimitInGb
        {
            get => ReadFloatSetting(nameof(MemoryLimitInGb));
            set => WriteSetting(nameof(MemoryLimitInGb), value);
        }

        [ConfigurationItem("retryTimeout", ProviderType.File)]
        public TimeSpan? RetryTimeout
        {
            get => ReadTimeSpanSetting(nameof(RetryTimeout));
            set => WriteSetting(nameof(RetryTimeout), value);
        }

        [ConfigurationItem("locale", ProviderType.ConfigurationManager)]
        public string? Locale
        {
            get => ReadSetting(nameof(Locale));
            set => WriteSetting(nameof(Locale), value);
        }
    }
}
