using I18n.Core.Core;
using I18n.Core.Wpf.Resources;

namespace I18n.Core.Wpf.Extensions
{
    public static class I18nConfiguration
    {
        private static LoadResources _loadResources;
        internal static LoadResources loadResources
        {
            get
            {
                if (_loadResources == null)
                {
                    _loadResources = new LoadResources();
                }
                return _loadResources;
            }
        }

        public static I18nCore I18nResourceAssembly(this I18nCore i18nCore, System.Reflection.Assembly assembly)
        {
            loadResources.I18nResourceAssembly(assembly);
            return i18nCore;
        }

        public static I18nCore I18nResourceNamespace(this I18nCore i18nCore, string namespaceName)
        {
            loadResources.I18nResourceNamespace(namespaceName);
            return i18nCore;
        }

        public static I18nCore I18nResourceDirectory(this I18nCore i18nCore, string directory)
        {
            loadResources.I18nResourceDirectory(directory);
            return i18nCore;
        }

        public static I18nCore I18nDefaultLang(this I18nCore i18nCore,string lang)
        {
            i18nCore.SetDefaultLanguage(lang);
            return i18nCore;
        }
    }
}
