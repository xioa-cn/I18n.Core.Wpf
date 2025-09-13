using I18n.Core.Core;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace I18n.Core.Wpf.Resources
{
    public static class I18nDicLoad
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static void LoadLanguageFromStream(this I18nCore i18nCore, Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                i18nCore._currentLangDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);

                i18nCore._currentLangDict = Flatten.FlattenDictionary(i18nCore._currentLangDict);
            }
        }

        public static void LoadLanguageFromFile(this I18nCore i18nCore, string filePath)
        {
            var json = System.IO.File.ReadAllText(filePath);

            i18nCore._currentLangDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);
           
            i18nCore._currentLangDict = Flatten.FlattenDictionary(i18nCore._currentLangDict);
        }
    }
}
