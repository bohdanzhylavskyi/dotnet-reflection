using dotnet_reflection.Exceptions;
using Shared;
using System.Reflection;

namespace dotnet_reflection
{
    internal class ConfigurationComponentBase
    {
        static private Dictionary<ProviderType, IConfigurationProvider> Providers = new();

        public static void ConfigureProviders(Dictionary<ProviderType, IConfigurationProvider> providers)
        {
            Providers = providers;
        }

        public string? ReadSetting(string propertyName)
        {
            PropertyInfo property = this.GetType().GetProperty(propertyName)!;

            var configItemAttr = property.GetCustomAttribute<ConfigurationItemAttribute>();

            if (configItemAttr == null)
            {
                throw AttributeNotAppliedToPropertyException(propertyName);
            }

            return ResolveConfigurationProvider(configItemAttr.Provider).Get(configItemAttr.SettingName);
        }

        public int? ReadIntSetting(string propertyName) => ReadSetting(propertyName) is string s ? int.Parse(s) : null;

        public float? ReadFloatSetting(string propertyName) => ReadSetting(propertyName) is string s ? float.Parse(s) : null;

        public TimeSpan? ReadTimeSpanSetting(string propertyName) => ReadSetting(propertyName) is string s ? TimeSpan.Parse(s) : null;

        public void WriteSetting<T>(string propertyName, T value)
        {
            PropertyInfo property = this.GetType().GetProperty(propertyName)!;

            var configItemAttr = property.GetCustomAttribute<ConfigurationItemAttribute>();

            if (configItemAttr == null)
            {
                throw AttributeNotAppliedToPropertyException(propertyName);
            }

            var provider = ResolveConfigurationProvider(configItemAttr.Provider);

            provider.Set(configItemAttr.SettingName, value.ToString());
            provider.Save();
        }

        private Exception AttributeNotAppliedToPropertyException(string propertyName)
        {
            return new ConfigurationItemAttrIsNotAppliedException(propertyName);
        }

        private IConfigurationProvider ResolveConfigurationProvider(ProviderType providerType)
        {
            if (Providers.TryGetValue(providerType, out IConfigurationProvider? provider))
            {
                return provider;
            }

            throw new MissingConfigurationProviderException(providerType);
        }
    }
}
