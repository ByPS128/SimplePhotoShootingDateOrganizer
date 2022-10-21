using System;
using System.Windows.Input;

namespace PhotoOrganiser
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;

        private Predicate<object> _canExecute;

        private event EventHandler _canExecuteChangedInternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _ = execute ?? throw new ArgumentNullException(nameof(execute));
            _ = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _canExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                _canExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this._canExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            _canExecute = _ => false;
            _execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
