using Shared;
using System.Configuration;
using System.Reflection;

namespace dotnet_reflection
{
    public class ConfigurationProvidersOptions : IConfigurationProvidersOptions
    {
        readonly static string ConfigurationFilePath = ".\\configs\\config.txt";

        public ConfigurationManagerConfigurationProviderOptions ForConfigurationManagerConfigurationProvider()
        {
            return new ConfigurationManagerConfigurationProviderOptions()
            {
                ConfigurationUserLevel = ConfigurationUserLevel.None
            };
        }

        public FileConfigurationProviderOptions ForFileConfigurationProvider()
        {
            return new FileConfigurationProviderOptions() { FilePath = ConfigurationFilePath };
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigureConfigurationProviders();

            var appSettings = new AppSettings();

            Console.WriteLine("Initial Settings:");
            PrintAllProperties(appSettings);

            appSettings.MaxRequestPerSecond = 120;
            appSettings.Locale = "ch";
            appSettings.RetryTimeout = TimeSpan.Parse("00:05:00");

            Console.WriteLine($"\nSettings After Update:");
            PrintAllProperties(appSettings);

            //var x = new FileConfigurationProvider.FileConfigurationProvider(ConfigurationFilePath);
        }

        private static void PrintAllProperties(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(
                    $"[Property Name]: {prop.Name}, " +
                    $"[Property Value]: {prop.GetValue(obj) ?? "null"}, " +
                    $"[Property Type]: {Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType}"
                );
            }
        }

        private static void ConfigureConfigurationProviders()
        {
            var options = new ConfigurationProvidersOptions();
            var providers = new Dictionary<ProviderType, IConfigurationProvider>();

            var pluginDlls = Directory.GetFiles("./ConfigurationProviders");

            foreach (var pluginDllPath in pluginDlls)
            {
                var asm = Assembly.LoadFrom(pluginDllPath);
                var configurationProviderBuilderType = asm.GetTypes().Where(t => typeof(IConfigurationProviderBuilder).IsAssignableFrom(t) && !t.IsInterface).FirstOrDefault();

                if (configurationProviderBuilderType == null)
                {
                    throw new InvalidOperationException($"Configuration Provider Builder is not defined in assembly '{pluginDllPath}'");
                }

                var builder = (IConfigurationProviderBuilder)Activator.CreateInstance(configurationProviderBuilderType)!;
                var provider = builder.Build(options);

                providers.Add(provider.GetProviderType(), provider);
            }

            ConfigurationComponentBase.ConfigureProviders(providers);
        }
    }
}
