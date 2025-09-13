using System.Windows.Input;

namespace I18n.Demo.Command
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
   
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T param)
            {
                return _canExecute == null || _canExecute(param);
            }

            return _canExecute == null && parameter == null && typeof(T).IsValueType == false;
        }

        public void Execute(object parameter)
        {
            if (parameter is T param)
            {
                _execute(param);
            }
        
            else if (parameter == null && typeof(T).IsValueType == false)
            {
                _execute(default);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
