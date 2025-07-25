﻿using Shared;

namespace FileConfigurationProvider
{
    public class FileConfigurationProvider : IConfigurationProvider
    {
        private readonly string filePath;
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        public ProviderType GetProviderType()
        {
            return ProviderType.File;
        }

        public FileConfigurationProvider(FileConfigurationProviderOptions options)
        {
            var filePath = options.FilePath;

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

            this.filePath = options.FilePath;
        }

        public string? Get(string key)
        {
            return settings.TryGetValue(key, out var value) ? value : null;
        }

        public void Save()
        {
            IEnumerable<string> lines = settings.AsEnumerable()
                .Select((pair) => $"{pair.Key}={pair.Value}");

            File.WriteAllLines(filePath, lines);
        }

        public void Set(string key, string value)
        {
            this.settings[key] = value;
        }
    }
}
