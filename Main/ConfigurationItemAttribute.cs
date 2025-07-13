using Shared;

namespace dotnet_reflection
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    internal class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; }
        public ProviderType Provider { get; }
        public ConfigurationItemAttribute(string settingName, ProviderType provider)
        {
            SettingName = settingName;
            Provider = provider;
        }
    }
}
