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
            PropertyInfo property = this.GetType().GetProperty(propertyName);

            var attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();

            if (attribute == null)
            {
                throw AttributeNotAppliedToPropertyException(propertyName);
            }

            return ResolveProvider(attribute.Provider).Get(attribute.SettingName);
        }

        public int? ReadIntSetting(string propertyName)
        {
            var x = ReadSetting(propertyName);

            return x == null ? null : int.Parse(x);
        }

        public float? ReadFloatSetting(string propertyName)
        {
            var x = ReadSetting(propertyName);

            return x == null ? null : float.Parse(x);
        }

        public TimeSpan? ReadTimeSpanSetting(string propertyName)
        {
            var x = ReadSetting(propertyName);

            return x == null ? null : TimeSpan.Parse(x);
        }

        public void WriteSetting<T>(string propertyName, T value)
        {
            PropertyInfo property = this.GetType().GetProperty(propertyName);

            var attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();

            if (attribute == null)
            {
                throw AttributeNotAppliedToPropertyException(propertyName);
            }

            var provider = ResolveProvider(attribute.Provider);

            provider.Set(attribute.SettingName, value.ToString());
            provider.Save();
        }

        private Exception AttributeNotAppliedToPropertyException(string propertyName)
        {
            return new InvalidOperationException($"The '{nameof(ConfigurationItemAttribute)}' attribute is not applied to property '{propertyName}'.");
        }

        private IConfigurationProvider ResolveProvider(ProviderType providerType)
        {
            return Providers[providerType];
        }
    }
}
