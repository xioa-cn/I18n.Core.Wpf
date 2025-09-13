using I18n.Core.Example;
using System.Windows.Data;

namespace I18n.Core.Wpf.Converter
{
    public class LocalizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string key;
            if (value is Binding binding)
            {
                key = binding.Path.Path;
            }
            else if (value is string str)
            {
                key = str;
                if (string.IsNullOrEmpty(key))
                {
                    return "[Invalid Key]";
                }
            }
            else
            {
                return "[Invalid Key]";
            }

            // 调用 I18nManager 获取本地化文本
            return I18nManager.Instance.GetString(key);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("本地化无需反向转换");
        }
    }

}
