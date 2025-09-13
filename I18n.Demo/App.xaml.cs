using I18n.Core.Example;
using I18n.Core.Models;
using I18n.Core.Wpf.Extensions;
using System.Reflection;
using System.Windows;

namespace I18n.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // 语言文件为程序资源的写法
            {
                // 配置语言管理器使用当前程序集作为资源来源
                I18nManager.Instance.I18nResourceAssembly(Assembly.GetExecutingAssembly());
                // 设置资源在当前程序集中的命名空间
                I18nManager.Instance.I18nResourceNamespace("I18n.Demo.Langs");
                // 设置默认语言资源文件 （这里为资源名称 排除后命名空间）
                I18nManager.Instance.DefaultLang("zh");
            }
            // 语言文件为外部文件的写法
            //{
            //    // 先设置为外部资源模式 JsonMode 默认为 OnApplicationResources
            //    I18nManager.Instance.JsonMode(I18nJsonMode.OnFileDir);
            //    // 设置资源文件所在的目录
            //    I18nManager.Instance.I18nResourceDirectory(
            //        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Langs")
            //    );
            //    // 设置默认语言文件
            //    I18nManager.Instance.DefaultLang("zh");
            //}


            // 初始化语言管理器
            I18nManager.Instance.Initialized();

            I18nManager.Instance.ExtensionValueFunc(ApplicationContentText);
        }

        public string ApplicationContentText(string key)
        {
            return $"[ApplicationContent~{key}]";
        }
    }

}
