using I18n.Core.Core;
using I18n.Core.Wpf.Extensions;
using System.IO;

namespace I18n.Core.Wpf.Resources
{
    public static class I18nAnalysisJson
    {
        public static void ChangeAppResourcesLanguage(this I18nCore i18nCore, string langCode)
        {
            i18nCore.UsingLanguage = langCode;
            if (I18nConfiguration.loadResources.ResourceAssembly == null)
                throw new InvalidOperationException("ResourceAssembly未设置，请先配置资源所在的程序集");

            if (string.IsNullOrEmpty(I18nConfiguration.loadResources.ResourceNamespace))
                throw new InvalidOperationException("ResourceNamespace未设置，请先配置资源路径");

            var resourceName = $"{I18nConfiguration.loadResources.ResourceNamespace}.{langCode}.json";
            resourceName = resourceName.Replace("..", ".");

            using (var stream = I18nConfiguration.loadResources.ResourceAssembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    i18nCore.LoadLanguageFromStream(stream);
                    i18nCore.LanguageChanged();
                    return;
                }
            }

            throw new FileNotFoundException($"找不到语言资源文件: {langCode}.json 和默认的 {i18nCore.DefaultLanguage}.json",
                $"{langCode}.json");
        }

        public static void ChangeFileDirLanguage(this I18nCore i18nCore, string langCode)
        {
            i18nCore.UsingLanguage = langCode;
            if (string.IsNullOrEmpty(I18nConfiguration.loadResources.ResourceDirectory))
            {
                throw new InvalidOperationException("请先设置ResourceDirectory属性");
            }

            var filePath = Path.Combine(I18nConfiguration.loadResources.ResourceDirectory, $"{langCode}.json");

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"找不到语言资源文件: {langCode}.json");
            }

            i18nCore.LoadLanguageFromFile(filePath);
           
            i18nCore.LanguageChanged();
        }
    }
}
