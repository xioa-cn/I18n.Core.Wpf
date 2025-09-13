using I18n.Core.Example;
using I18n.Core.Models;
using I18n.Core.Wpf.Extensions;
using I18n.Demo.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace I18n.Demo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler? PropertyChanged;


        public ObservableCollection<LangSource> LangSources { get; set; }


        public MainViewModel()
        {
            LangSources =
            [
                new LangSource() { Name = "中文", SourceKey = "zh" },
                new LangSource() { Name = "English", SourceKey = "en" },
            ];
            ChangeLangCommand = new RelayCommand<LangSource>(ChangeLang);
        }

        private void ChangeLang(LangSource source)
        {
            I18nManager.Instance.ChangeLanguage(source.SourceKey);
        }

        public ICommand ChangeLangCommand { get; set; }

    }
}
