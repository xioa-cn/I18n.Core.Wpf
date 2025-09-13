using I18n.Core.Example;
using I18n.Core.Wpf.Resources;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace I18n.Core.Wpf.Extensions
{
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizeExtension : MarkupExtension
    {
        private string _key;
        private DependencyObject _targetObject;
        private DependencyProperty _targetProperty;

        public LocalizeExtension(string key)
        {
            _key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {        
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
         
            if (target?.TargetObject is DependencyObject targetObject &&
                target.TargetProperty is DependencyProperty targetProperty)
            {
                _targetObject = targetObject;
                _targetProperty = targetProperty;
              
                WeakReference<DependencyObject> weakTarget = new WeakReference<DependencyObject>(targetObject);
                Action updateAction = () =>
                {
                    if (weakTarget.TryGetTarget(out var obj) && _targetProperty != null)
                    {
                        obj.SetValue(_targetProperty, GetLocalizedValue());
                    }
                };
         
                I18nManager.Instance.OnLanguageChanged += updateAction;
           
                var cleanup = new CleanupHandler(weakTarget, updateAction);
                DependencyPropertyDescriptor.FromProperty(FrameworkElement.DataContextProperty, typeof(FrameworkElement))
                    ?.AddValueChanged(targetObject, cleanup.OnTargetDestroyed);
            }

            return GetLocalizedValue();
        }

        private string GetLocalizedValue()
        {
            return I18nManager.Instance.GetString(_key) ?? $"[{_key}]";
        }             
    }
}
