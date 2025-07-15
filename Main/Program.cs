using dotnet_reflection.Exceptions;
using Shared;
using System.Reflection;

namespace dotnet_reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SetupConfigurationProviders();

            var appSettings = new AppSettings();

            Console.WriteLine("Initial Settings:");
            PrintAllSettings(appSettings);

            appSettings.MaxRequestPerSecond = 120;
            appSettings.Locale = "uk";
            appSettings.RetryTimeout = TimeSpan.Parse("00:05:00");
            appSettings.MemoryLimitInGb = 1.5f;

            Console.WriteLine($"\nSettings After Update:");
            PrintAllSettings(appSettings);
        }

        private static void PrintAllSettings(AppSettings appSettings)
        {
            var properties = appSettings.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var configItemAttr = prop.GetCustomAttribute<ConfigurationItemAttribute>();

                if (configItemAttr != null) // process cofiguration properties only
                {
                    Console.WriteLine(
                        $"[Property Name]: {prop.Name}, " +
                        $"[Property Value]: {prop.GetValue(appSettings) ?? "null"}, " +
                        $"[Property Type]: {Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType}, " +
                        $"[Provider Type]: {configItemAttr.Provider}"
                    );
                }
            }
        }

        private static void SetupConfigurationProviders()
        {
            var options = new ConfigurationProvidersOptions();
            var providers = new Dictionary<ProviderType, IConfigurationProvider>();

            foreach (var providerPluginAsm in LoadConfigurationProviderAssemblies())
            {
                var configurationProviderBuilderType = providerPluginAsm.GetTypes()
                    .Where(t => typeof(IConfigurationProviderBuilder).IsAssignableFrom(t) && !t.IsInterface)
                    .FirstOrDefault();

                if (configurationProviderBuilderType == null)
                {
                    throw new ConfigurationProviderBuilderIsNotDefined(providerPluginAsm.Location);
                }

                var builder = (IConfigurationProviderBuilder)Activator.CreateInstance(configurationProviderBuilderType)!;
                var provider = builder.Build(options);

                providers.Add(provider.GetProviderType(), provider);
            }

            ConfigurationComponentBase.ConfigureProviders(providers);
        }

        private static IEnumerable<Assembly> LoadConfigurationProviderAssemblies()
        {
            var pluginsFolderAsbPath = Path.Combine(
               Path.GetDirectoryName(typeof(Program).Assembly.Location),
               "ConfigurationProviders"
            );

            var plugins = Directory.GetFiles(pluginsFolderAsbPath);

            return Directory.GetFiles(pluginsFolderAsbPath)
                            .Select((pluginPath) =>
                                new ConfigurationProviderPluginLoadContext(pluginPath).LoadFromAssemblyPath(pluginPath)
                            );
        }
    }
}
