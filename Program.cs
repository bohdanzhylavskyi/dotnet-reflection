using dotnet_reflection.ConfigurationProviders;
using System.Reflection;

namespace dotnet_reflection
{
    internal class Program
    {
        readonly static string ConfigurationFilePath = "C:\\Users\\Bohdan_Zhylavskyi\\Desktop\\config.txt";

        static void Main(string[] args)
        {
            ConfigureConfigurationProviders();

            var appSettings = new AppSettings();

            Console.WriteLine("Initial Settings:");
            PrintAllProperties(appSettings);

            appSettings.MaxRequestPerSecond = 120;
            appSettings.Locale = "uk";
            appSettings.RetryTimeout = TimeSpan.Parse("00:05:00");

            Console.WriteLine($"\nSettings After Update:");
            PrintAllProperties(appSettings);
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
            var providers = new Dictionary<ProviderType, IConfigurationProvider>()
            {
                { ProviderType.File, new FileConfigurationProvider(ConfigurationFilePath)},
            };

            ConfigurationComponentBase.ConfigureProviders(providers);
        }
    }
}
