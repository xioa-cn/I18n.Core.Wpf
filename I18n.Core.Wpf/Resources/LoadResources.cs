using System.Reflection;

namespace I18n.Core.Wpf.Resources
{
    internal sealed class LoadResources
    {
        internal LoadResources()
        {
            
        }

        internal Assembly ResourceAssembly { get; private set; }

        internal void I18nResourceAssembly(Assembly assembly)
        {
            ResourceAssembly = assembly;
        }

        internal string ResourceNamespace { get; private set; }

        internal void I18nResourceNamespace(string namespaceName)
        {
            ResourceNamespace = namespaceName;
        }

        internal string ResourceDirectory { get; private set; }

        internal void I18nResourceDirectory(string directory)
        {
            ResourceDirectory = directory;
        }
    }
}
