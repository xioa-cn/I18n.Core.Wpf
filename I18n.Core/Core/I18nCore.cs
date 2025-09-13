using I18n.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace I18n.Core.Core
{
    public sealed class I18nCore
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


        internal I18nCore()
        {

        }

        internal Dictionary<string, object> _currentLangDict;

        public event Action OnLanguageChanged;

        public void LanguageChanged()
        {
            if (OnLanguageChanged != null)
                this.OnLanguageChanged.Invoke();
        }

        private Func<string, string> _extensionKeyValue;

        public void ExtensionValueFunc(Func<string, string> func)
        {
            _extensionKeyValue = func;
        }

        public I18nJsonMode I18NJsonMode { get; private set; } = I18nJsonMode.OnApplicationResources;

        public void JsonMode(I18nJsonMode mode)
        {
            I18NJsonMode = mode;
        }

        public string DefaultLanguage { get; private set; }

        public void DefaultLang(string lang)
        {
            DefaultLanguage = lang;
        }

        public Func<string, string> CustomJsonFunc { get; private set; }

        public void CustomJsonLoader(Func<string, string> func)
        {
            CustomJsonFunc = func;
        }

        public void CustomChangeLanguage(string key)
        {
            if (CustomJsonFunc != null)
            {
                var json = CustomJsonFunc(key);
                _currentLangDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);

                _currentLangDict = Flatten.FlattenDictionary(_currentLangDict);
                LanguageChanged();
                return;
            }

            throw new InvalidOperationException("CustomJsonFunc未设置，请先调用CustomJsonLoader方法设置");
        }

        public void SetDefaultLanguage(string lang)
        {
            DefaultLanguage = lang;
        }

        public string UsingLanguage { get; set; }

        public string GetString(string key)
        {
            if (_currentLangDict?.TryGetValue(key, out var value) == true)
            {
                return value.ToString();
            }

            if (this._extensionKeyValue != null)
            {
                return this._extensionKeyValue(key);
            }

            return $"[{key}]";
        }
    }
}
