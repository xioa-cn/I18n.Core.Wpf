using I18n.Core.Example;
using I18n.Core.Models;
using I18n.Core.Wpf.Extensions;
using I18n.Demo.Command;
using I18n.Demo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace I18n.Demo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler? PropertyChanged;


        public ObservableCollection<LangSource> LangSources { get; set; }

        private ComboBoxSelectValueEnum _TestValue;

        public ComboBoxSelectValueEnum TestValue
        {
            get => _TestValue;
            set
            {
                _TestValue = value;
                OnPropertyChanged(nameof(TestValue));
            }
        }

        public List<ComboBoxItem> ComboBoxItems { get; } = new();
        public MainViewModel()
        {
            LangSources =
            [
                new LangSource() { Name = "中文", SourceKey = "zh" },
                new LangSource() { Name = "English", SourceKey = "en" },
            ];
            ChangeLangCommand = new RelayCommand<LangSource>(ChangeLang);


            var itemList = new List<(string Key, ComboBoxSelectValueEnum Value)>
                        {
                            ("ComboBox.Value1", ComboBoxSelectValueEnum.None),
                            ("ComboBox.Value2", ComboBoxSelectValueEnum.Test1)
                        };

            foreach (var (key, enumValue) in itemList)
            {
                var item = new ComboBoxItem();
                var ext = new LocalizeExtension(key);

                var serviceProvider = new ServiceProvider(item, ComboBoxItem.ContentProperty);
                item.Content = ext.ProvideValue(serviceProvider);
                item.Tag = enumValue;
                ComboBoxItems.Add(item);
            }
        }

        private void ChangeLang(LangSource source)
        {
            I18nManager.Instance.ChangeLanguage(source.SourceKey);
        }

        public ICommand ChangeLangCommand { get; set; }

    }


    public class ServiceProvider : IServiceProvider, IProvideValueTarget
    {
        private readonly object _targetObject;
        private readonly object _targetProperty;

        public ServiceProvider(object targetObject, object targetProperty)
        {
            _targetObject = targetObject;
            _targetProperty = targetProperty;
        }

        public object GetService(Type serviceType)
        {
            // 你的 LocalizeExtension 只需要这个服务，所以直接返回自己
            if (serviceType == typeof(IProvideValueTarget))
                return this;
            return null;
        }

        public object TargetObject => _targetObject;
        public object TargetProperty => _targetProperty;
    }
}
