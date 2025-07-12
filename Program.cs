using dotnet_reflection.ConfigurationProviders;
using System.Reflection;

namespace dotnet_reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConfigureConfigurationProviders();

            var appSettings = new AppSettings();

            Console.WriteLine(appSettings.Limit);

            appSettings.Limit = 120;

            Console.WriteLine($"After change: {appSettings.Limit + 200}");
        }

        private static void ConfigureConfigurationProviders()
        {
            var fp = ConfigureFileConfigurationProvider();
            var providers = new Dictionary<ProviderType, IConfigurationProvider>()
            {
                { ProviderType.File, fp},
            };

            ConfigurationComponentBase.ConfigureProviders(providers);
        }

        private static IConfigurationProvider ConfigureFileConfigurationProvider()
        {
            var filePath = "C:\\Users\\Bohdan_Zhylavskyi\\Desktop\\config.txt";
            var fp = new FileConfigurationProvider(filePath);

            return fp;
        }
    }
}
