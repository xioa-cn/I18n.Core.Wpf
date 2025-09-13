using I18n.Core.Example;
using System.Windows;

namespace I18n.Core.Wpf.Resources
{
    public class CleanupHandler
    {
        private readonly WeakReference<DependencyObject> _weakTarget;
        private readonly Action _updateAction;

        public CleanupHandler(WeakReference<DependencyObject> weakTarget, Action updateAction)
        {
            _weakTarget = weakTarget;
            _updateAction = updateAction;
        }

        public void OnTargetDestroyed(object sender, EventArgs e)
        {
            if (!_weakTarget.TryGetTarget(out _))
            {
                I18nManager.Instance.OnLanguageChanged -= _updateAction;
            }
        }
    }
}
