using System.Reflection;
using System.Runtime.Loader;

namespace dotnet_reflection
{
    public class ConfigurationProviderPluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver resolver;

        public ConfigurationProviderPluginLoadContext(string pluginPath)
        {
            resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
            
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            throw new FileNotFoundException($"Assembly '{assemblyName.FullName}' is not found");
        }
    }
}
