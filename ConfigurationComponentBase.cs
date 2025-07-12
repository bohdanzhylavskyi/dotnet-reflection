using dotnet_reflection.ConfigurationProviders;
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

        public T ReadSetting<T>(string propertyName)
        {
            PropertyInfo property = this.GetType().GetProperty(propertyName);

            var attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();
            var propertyType = property.PropertyType;
            MethodInfo parseMethod = propertyType.GetMethod("Parse", new[] { typeof(string) });

            if (parseMethod == null)
            {
                throw new Exception("");
            }

            if (attribute != null)
            {
                Console.WriteLine($"Read setting: {propertyName}, attributeSettingName: {attribute.SettingName}, attributeProvider: {attribute.Provider}");


                var provider = ResolveProvider(attribute.Provider);

                var obj = parseMethod.Invoke(null, new object[] { provider.Get(attribute.SettingName) });

                return (T)obj;

            }


            return (T)(object)null;
        }

        public void WriteSetting<T>(string propertyName, T value)
        {
            PropertyInfo property = this.GetType().GetProperty(propertyName);

            var attribute = property.GetCustomAttribute<ConfigurationItemAttribute>();

            if (attribute != null)
            {
                Console.WriteLine($"Write setting: {propertyName}, value: {value}, attributeSettingName: {attribute.SettingName}, attributeProvider: {attribute.Provider}");
                var provider = ResolveProvider(attribute.Provider);

                provider.Set(attribute.SettingName, value.ToString());
                provider.Save();
            }
        }

        private IConfigurationProvider ResolveProvider(ProviderType providerType)
        {
            return Providers[providerType];
        }
    }
}
