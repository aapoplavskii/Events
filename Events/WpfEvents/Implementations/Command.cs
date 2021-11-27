using System;
using System.Windows.Input;

namespace WpfEvents.Implementations
{
    public class Command : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _onInvoke;

        public Command(Action onInvoke)
        {
            _onInvoke = onInvoke;
        }

        public Command(Action onInvoke, Func<bool> canExecute)
        {
            _onInvoke = onInvoke;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_onInvoke == null || _canExecute == null)
                    return;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_onInvoke == null || _canExecute == null)
                    return;
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter) || _onInvoke == null)
                return;

            _onInvoke.Invoke();
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public class Command<T> : ICommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _onInvoke;

        public Command(Action<T> onInvoke)
        {
            _onInvoke = onInvoke;
        }

        public Command(Action<T> onInvoke, Func<T, bool> canExecute)
        {
            _onInvoke = onInvoke;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_onInvoke == null) return;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_onInvoke == null) return;
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if (parameter == null && typeof(T).IsValueType)
                return _canExecute.Invoke(default(T));

            return _canExecute.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            var parameter1 = parameter;

            if (parameter != null && parameter.GetType() != typeof(T) && parameter is IConvertible)
                parameter1 = Convert.ChangeType(parameter, typeof(T), null);

            if (!CanExecute(parameter1) || _onInvoke == null)
                return;

            if (parameter1 == null)
            {
                if (typeof(T).IsValueType)
                    _onInvoke.Invoke(default(T));
                else
                    _onInvoke.Invoke((T)parameter1);
            }
            else
                _onInvoke.Invoke((T)parameter1);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
