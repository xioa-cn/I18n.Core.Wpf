using I18n.Core.Core;
using I18n.Core.Models;
using I18n.Core.Wpf.Resources;

namespace I18n.Core.Wpf.Extensions
{
    public static class I18nChangeLang
    {
        public static void ChangeLanguage(this I18nCore i18nCore, string langCode)
        {
            if (i18nCore.CustomJsonFunc != null)
            {
                i18nCore.CustomChangeLanguage(langCode);
            }
            else if (i18nCore.I18NJsonMode == I18nJsonMode.OnApplicationResources)
            {
                i18nCore.ChangeAppResourcesLanguage(langCode);
            }
            else if (i18nCore.I18NJsonMode == I18nJsonMode.OnFileDir)
            {
                i18nCore.ChangeFileDirLanguage(langCode);
            }
        }
    }
}
