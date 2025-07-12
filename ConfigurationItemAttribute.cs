namespace dotnet_reflection
{
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
