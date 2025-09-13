using System.Collections.Generic;
using System.Text.Json;

namespace I18n.Core.Core
{
    public static class Flatten
    {
        public static Dictionary<string, object> FlattenDictionary(Dictionary<string, object> dict, string prefix = "")
        {          
            var result = new Dictionary<string, object>();
          
            foreach (var item in dict)
            {              
                var key = string.IsNullOrEmpty(prefix) ? item.Key : $"{prefix}.{item.Key}";
              
                if (item.Value is JsonElement element && element.ValueKind == JsonValueKind.Object)
                {
                    var nestedDict = JsonSerializer.Deserialize<Dictionary<string, object>>(element.GetRawText());
                    var flattened = FlattenDictionary(nestedDict, key);
                
                    foreach (var nestedItem in flattened)
                    {
                        result[nestedItem.Key] = nestedItem.Value;
                    }
                }
                else
                {
                    result[key] = item.Value?.ToString() ?? string.Empty;
                }
            }
           
            return result;
        }

    }
}
