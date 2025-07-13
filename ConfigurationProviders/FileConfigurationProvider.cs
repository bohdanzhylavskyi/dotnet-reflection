namespace dotnet_reflection.ConfigurationProviders
{
    internal class FileConfigurationProvider : IConfigurationProvider
    {
        private readonly string FilePath;
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        public FileConfigurationProvider(string filePath)
        {
            FilePath = filePath;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{filePath}' does not exist");
            }

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split("=");

                if (parts.Length == 2)
                    settings[parts[0]] = parts[1];
            }
        }

        public string? Get(string key)
        {
            return settings.TryGetValue(key, out var value) ? value : null;
        }

        public void Save()
        {
            IEnumerable<string> lines = settings.AsEnumerable().Select((pair) =>
            {
                return $"{pair.Key}={pair.Value}";
            });

            File.WriteAllLines(FilePath, lines);
        }

        public void Set(string key, string value)
        {
            this.settings[key] = value;
        }
    }
}
