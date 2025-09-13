using I18n.Core.Core;

namespace I18n.Core.Wpf.Extensions
{
    public static class I18nInitialized
    {
        public static I18nCore Initialized(this I18nCore i18nCore, string? lang = null)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                i18nCore.UsingLanguage = lang;
            }
            else
            {
                i18nCore.UsingLanguage = i18nCore.DefaultLanguage;
            }

            if (string.IsNullOrEmpty(i18nCore.UsingLanguage))
            {
                throw new InvalidOperationException("未设置默认语言，请先调用 I18nDefaultLang 方法设置默认语言");
            }

            i18nCore.ChangeLanguage(i18nCore.UsingLanguage);
            return i18nCore;
        }
    }
}
